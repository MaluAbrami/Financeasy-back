using System.Text.Json.Serialization;
using Financeasy.Domain.Enums;

namespace Financeasy.Domain.DTO
{
    public record FinancialEntryUpdateDTO
    {
        public decimal? Amount { get; set; }
        public string? Description { get; set; } = string.Empty;
        public DateTime? Date { get; set; }
    }
}