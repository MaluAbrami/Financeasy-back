using Financeasy.Domain.Enums;

namespace Financeasy.Domain.DTO
{
    public class FinancialEntryUpdateDTO
    {
        public decimal? Amount { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; } = string.Empty;
        public DateTime? Date { get; set; }
        public EntryType? Type { get; set; }
        public bool? Fixed { get; set; }
    }
}