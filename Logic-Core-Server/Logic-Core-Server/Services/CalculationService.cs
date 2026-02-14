using DynamicExpresso;

namespace Logic_Core_Server.Services;

// שימוש ב-Primary Constructor: מזריקים ישירות בשורת הגדרת המחלקה
public class CalculationService(AppDbContext context, Interpreter interpreter) : ICalculationService
{
    public async Task<CalculationResponse> CalculateAsync(string operationKey, double a, double b)
    {
        // 1. שליפת סוג הפעולה
        var operation = await context.Operations
            .FirstOrDefaultAsync(op => op.Key == operationKey && op.IsActive);

        if (operation == null) throw new KeyNotFoundException("פעולה לא נמצאה");

        // 2. הבאת נתונים סטטיסטיים *לפני* השמירה (חוסך Skip/Take)
        var lastThree = await GetLastThreeHistoryAsync(operationKey, operation.Name, operation.Symbol);
        int monthlyCount = await GetMonthlyUsageCountAsync(operationKey);

        // 3. ביצוע החישוב
        double result = ExecuteDynamicCalculation(operation.Formula, a, b);

        // 4. שמירת הלוג ב-DB (פעולה אחת בלבד)
        context.CalculationLogs.Add(new CalculationLog
        {
            OperationKey = operationKey,
            InputA = a,
            InputB = b,
            Result = result
        });
        await context.SaveChangesAsync();

        return new CalculationResponse
        {
            Value = result,
            MonthlyUsage = monthlyCount,
            HistoryLog = lastThree
        };
    }

    // פונקציה נפרדת לחישוב הדינמי - אחריות אחת
    private double ExecuteDynamicCalculation(string formula, double a, double b)
    {
        return interpreter.Eval<double>(formula, new[]
        {
            new Parameter("arg1", a),
            new Parameter("arg2", b)
        });
    }

    // פונקציה נפרדת להבאת היסטוריה - אחריות אחת
    private async Task<List<HistoryLogDto>> GetLastThreeHistoryAsync(string operationKey, string opName, string opSymbol)
    {
        return await context.CalculationLogs
            .Where(l => l.OperationKey == operationKey)
            .OrderByDescending(l => l.CreatedAt)
            .Take(3) // לוקחים את ה-3 האחרונים הקיימים (לפני השמירה החדשה)
            .Select(l => new HistoryLogDto
            {
                OperationName = opName,
                Symbol = opSymbol,
                InputA = l.InputA,
                InputB = l.InputB,
                Result = l.Result
            }).ToListAsync();
    }

    // פונקציה נפרדת לחישוב כמות חודשית - אחריות אחת
    private async Task<int> GetMonthlyUsageCountAsync(string operationKey)
    {
        var startOfMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
        return await context.CalculationLogs
            .CountAsync(l => l.OperationKey == operationKey && l.CreatedAt >= startOfMonth);
    }

    public async Task<List<OperationType>> GetAvailableOperationsAsync()
    {
        return await context.Operations.Where(o => o.IsActive).ToListAsync();
    }
}