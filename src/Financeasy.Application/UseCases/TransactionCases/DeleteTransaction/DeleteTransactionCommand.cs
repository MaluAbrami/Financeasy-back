using MediatR;

namespace Financeasy.Application.UseCases.TransactionCases.DeleteTransaction
{
    public record DeleteTransactionCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public Guid TransactionId { get; set; }
    }
}