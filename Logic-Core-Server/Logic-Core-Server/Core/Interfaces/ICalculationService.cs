namespace Logic_Core_Server.Core.Interfaces;

    public interface ICalculationService
    {
        Task<CalculationResponse> CalculateAsync(string operationKey, string a, string b);
        Task<List<OperationType>> GetAvailableOperationsAsync();
    }

