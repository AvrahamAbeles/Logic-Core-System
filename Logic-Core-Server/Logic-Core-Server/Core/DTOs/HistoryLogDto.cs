namespace Logic_Core_Server.Core.DTOs
{
    public class HistoryLogDto
    {
        public string OperationName { get; set; }
        public string Symbol { get; set; } 
        public string InputA { get; set; }
        public string InputB { get; set; }
        public string Result { get; set; }
    }
}
