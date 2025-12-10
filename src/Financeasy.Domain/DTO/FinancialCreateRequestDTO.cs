using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Financeasy.Domain.Enums;

namespace Financeasy.Domain.DTO
{
    public class FinancialCreateRequestDTO
    {
        public decimal Amount { get; set; }
        public required  string Category { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [Required(ErrorMessage = "Tipo é obrigatório")]
        public EntryType Type { get; set; }
        public bool IsFixed { get; set; }
    }
}