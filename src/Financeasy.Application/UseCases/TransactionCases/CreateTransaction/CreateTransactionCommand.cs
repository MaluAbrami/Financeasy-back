using Financeasy.Domain.Enums;
using MediatR;

namespace Financeasy.Application.UseCases.TransactionCases.CreateTransaction
{
    public record CreateTransactionCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid BankAccountId { get; set; }
        public Guid CategoryId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; } = string.Empty;
    }
}