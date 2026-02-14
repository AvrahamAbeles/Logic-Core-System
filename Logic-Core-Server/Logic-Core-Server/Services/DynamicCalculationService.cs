using DynamicExpresso;
using Logic_Core_Server.Core.DTOs;
using Logic_Core_Server.Core.Interfaces;
using Logic_Core_Server.Data.Context;
using Logic_Core_Server.Data.Entities;
using Microsoft.EntityFrameworkCore;
namespace Logic_Core_Server.Services
{
    public class DynamicCalculationService : ICalculationService
    {
        private readonly AppDbContext _context;
        private readonly Interpreter _interpreter;

        public DynamicCalculationService(AppDbContext context ,Interpreter interpreter)
        {
            _context = context;
            _interpreter = interpreter;
        }

        public async Task<CalculationResponse> CalculateAsync(string operationKey, double a, double b)
        {
            // שליפת הנוסחה מה-DB
            var operation = await _context.Operations
                .FirstOrDefaultAsync(op => op.Key == operationKey && op.IsActive);

            if (operation == null) throw new Exception("פעולה לא נמצאה");

            // הפעלת מנוע החישוב הדינמי
            double result = _interpreter.Eval<double>(operation.Formula, new[] { new Parameter("arg1", a), new Parameter("arg2", b) }); await _context.SaveChangesAsync();

            // שמירת תיעוד ב-Logs
            _context.CalculationLogs.Add(new CalculationLog { OperationKey = operationKey, InputA = a, InputB = b, Result = result });
            await _context.SaveChangesAsync(); // כאן החישוב נכנס ל-DB

            // 3. חישוב סטטיסטיקות לאפיון:

            // כמות פעולות מאותו סוג מתחילת החודש הנוכחי
            var now = DateTime.UtcNow;
            var startOfMonth = new DateTime(now.Year, now.Month, 1);
            int monthlyCount = await _context.CalculationLogs
                .CountAsync(l => l.OperationKey == operationKey && l.CreatedAt >= startOfMonth);
            // פירוט 3 פעולות אחרונות מאותו סוג
            var lastThree = await _context.CalculationLogs
        .Where(l => l.OperationKey == operationKey)
        .OrderByDescending(l => l.CreatedAt)
        .Skip(1) // הדילוג הקריטי
        .Take(3)
        .Select(l => new HistoryLogDto
        {
            OperationName = operation.Name,
            Symbol = operation.Symbol,
            InputA = l.InputA,
            InputB = l.InputB,
            Result = l.Result
        }).ToListAsync();

            return new CalculationResponse { Value = result, MonthlyUsage = monthlyCount, HistoryLog = lastThree };
        }

        public async Task<List<OperationType>> GetAvailableOperationsAsync()
        {
            return await _context.Operations.Where(o => o.IsActive).ToListAsync();
        }
    }
}
