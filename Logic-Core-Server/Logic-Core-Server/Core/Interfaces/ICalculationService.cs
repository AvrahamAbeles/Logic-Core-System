namespace Logic_Core_Server.Core.Interfaces;

    public interface ICalculationService
    {
        Task<CalculationResponse> CalculateAsync(string operationKey, double a, double b);
        Task<List<OperationType>> GetAvailableOperationsAsync();
    }

