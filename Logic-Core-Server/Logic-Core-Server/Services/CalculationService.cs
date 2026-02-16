using Azure;
using DynamicExpresso;
using System.Text.RegularExpressions;

namespace Logic_Core_Server.Services;

public class CalculationService(AppDbContext context, Interpreter interpreter) : ICalculationService
{
    public async Task<CalculationResponse> CalculateAsync(string operationKey, string a, string b)
    {
        var operation = await context.Operations
            .FirstOrDefaultAsync(op => op.Key == operationKey && op.IsActive);

        if (operation == null) throw new KeyNotFoundException("פעולה לא נמצאה");

        if (!string.IsNullOrEmpty(operation.ValidationRegex))
        {
            if (!Regex.IsMatch(a, operation.ValidationRegex))
            {
                throw new ArgumentException($"Error in field A: {operation.ValidationMessage ?? "קלט לא תקין"}");
            }

            if (!Regex.IsMatch(b, operation.ValidationRegex))
            {
                throw new ArgumentException($"Error in field B: {operation.ValidationMessage ?? "קלט לא תקין"}");
            }
        }




        var lastThree = await GetLastThreeHistoryAsync(operationKey, operation.Name, operation.Symbol);
        int monthlyCount = await GetMonthlyUsageCountAsync(operationKey);

        object result = ExecuteDynamicCalculation(operation.Formula, a, b);
        string resultString = result?.ToString() ?? "";

        context.CalculationLogs.Add(new CalculationLog
        {
            OperationKey = operationKey,
            InputA = a,
            InputB = b,
            Result = resultString
        });
        await context.SaveChangesAsync();

        return new CalculationResponse
        {
            Value = resultString,
            MonthlyUsage = monthlyCount,
            HistoryLog = lastThree
        };
    }

    private object ExecuteDynamicCalculation(string formula, string a, string b)
    {
        object isNumA = double.TryParse(a, out double valA) ? valA : a;
        object isNumB = double.TryParse(b, out double valB) ? valB : b;


        try
        {         
                return interpreter.Eval(formula, new[]
                {
                    new Parameter("arg1", valA),
                    new Parameter("arg2", valB)

                });

  
            
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"לא ניתן לבצע את הפעולה  על הערכים שהוזנו" );
        }
      
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