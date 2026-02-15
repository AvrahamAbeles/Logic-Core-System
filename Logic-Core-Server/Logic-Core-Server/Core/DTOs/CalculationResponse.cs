namespace Logic_Core_Server.Core.DTOs
{
    public class CalculationResponse
    {
        public string Value { get; set; }           // התוצאה הנוכחית
        public int MonthlyUsage { get; set; }       // כמות פעולות החודש
        public List<HistoryLogDto> HistoryLog { get; set; } // 3 פעולות אחרונות
    }
}
