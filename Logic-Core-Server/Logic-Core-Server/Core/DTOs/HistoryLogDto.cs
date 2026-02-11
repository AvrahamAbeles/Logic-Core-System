namespace Logic_Core_Server.Core.DTOs
{
    public class HistoryLogDto
    {
        public string OperationName { get; set; }
        public string Symbol { get; set; } 
        public double InputA { get; set; }
        public double InputB { get; set; }
        public double Result { get; set; }
    }
}
