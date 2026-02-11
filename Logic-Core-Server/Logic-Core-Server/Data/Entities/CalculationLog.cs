namespace Logic_Core_Server.Data.Entities
{
    public class CalculationLog
    {
        public int Id { get; set; }

        public string OperationKey { get; set; } 

        public double InputA { get; set; }
        public double InputB { get; set; }
        public double Result { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
