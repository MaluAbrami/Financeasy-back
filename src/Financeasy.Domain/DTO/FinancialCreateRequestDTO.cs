using Financeasy.Domain.models;

namespace Financeasy.Domain.DTO
{
    public record FinancialCreateRequestDTO
    {
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public required Guid CategoryId { get; set; }
    }
}