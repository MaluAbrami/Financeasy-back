using Financeasy.Domain.Enums;

namespace Financeasy.Domain.DTO.Transaction
{
    public record CreateTransactionRequest
    {
        public Guid BankAccountId { get; set; }
        public Guid CategoryId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; } = string.Empty;
    }
}