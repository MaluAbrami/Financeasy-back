using Financeasy.Domain.Enums;

namespace Financeasy.Domain.DTO
{
    public class FinancialUpdateRequestDTO
    {
        public decimal? Amount { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public EntryType? Type { get; set; }
        public bool? IsFixed { get; set; }
    }
}