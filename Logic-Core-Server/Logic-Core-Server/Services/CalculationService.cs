using DynamicExpresso;

namespace Logic_Core_Server.Services;

public class CalculationService(AppDbContext context, Interpreter interpreter) : ICalculationService
{
    public async Task<CalculationResponse> CalculateAsync(string operationKey, double a, double b)
    {
        var operation = await context.Operations
            .FirstOrDefaultAsync(op => op.Key == operationKey && op.IsActive);

        if (operation == null) throw new KeyNotFoundException("פעולה לא נמצאה");

        var lastThree = await GetLastThreeHistoryAsync(operationKey, operation.Name, operation.Symbol);
        int monthlyCount = await GetMonthlyUsageCountAsync(operationKey);

        double result = ExecuteDynamicCalculation(operation.Formula, a, b);

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

    private double ExecuteDynamicCalculation(string formula, double a, double b)
    {
        return interpreter.Eval<double>(formula, new[]
        {
            new Parameter("arg1", a),
            new Parameter("arg2", b)
        });
    }

    private async Task<List<HistoryLogDto>> GetLastThreeHistoryAsync(string operationKey, string opName, string opSymbol)
    {
        return await context.CalculationLogs
            .Where(l => l.OperationKey == operationKey)
            .OrderByDescending(l => l.CreatedAt)
            .Take(3) 
            .Select(l => new HistoryLogDto
            {
                OperationName = opName,
                Symbol = opSymbol,
                InputA = l.InputA,
                InputB = l.InputB,
                Result = l.Result
            }).ToListAsync();
    }

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