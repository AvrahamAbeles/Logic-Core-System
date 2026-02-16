using System.ComponentModel.DataAnnotations;

namespace Logic_Core_Server.Data.Entities
{
    public class OperationType
    {
        [Key]
        public string Key { get; set; }      

        [Required]
        public string Name { get; set; }     

        [Required]
        public string Formula { get; set; }
        public string Symbol { get; set; } 
        public bool IsActive { get; set; } = true;
        public string? ValidationRegex { get; set; } 
        public string? ValidationMessage { get; set; } 
    }
}
