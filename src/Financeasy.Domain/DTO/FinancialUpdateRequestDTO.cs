namespace Financeasy.Domain.DTO
{
    public record FinancialUpdateRequestDTO
    {
        public decimal? Amount { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
    }
}