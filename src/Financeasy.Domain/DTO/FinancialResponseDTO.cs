using Financeasy.Domain.Enums;

namespace Financeasy.Domain.DTO
{
    public record FinancialResponseDTO()
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public required string CategoryName { get; set; }
        public EntryType Type { get; set; }
        public bool IsFixed { get; set; }
    }
}