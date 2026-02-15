namespace Logic_Core_Server.Data.Entities
{
    public class CalculationLog
    {
        public int Id { get; set; }

        public string OperationKey { get; set; } 

        public string InputA { get; set; }
        public string InputB { get; set; }
        public string Result { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
