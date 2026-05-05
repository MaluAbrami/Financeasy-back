using Financeasy.Domain.Enums;

namespace Financeasy.Domain.DTO.Transaction
{
    public record GetTransactionResponseDTO
    {
        public Guid Id { get; set; }
        public string BankAccountName { get; set; }
        public string CategoryName { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
    }
}