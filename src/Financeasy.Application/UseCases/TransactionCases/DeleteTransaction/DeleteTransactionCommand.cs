using MediatR;

namespace Financeasy.Application.UseCases.TransactionCases.DeleteTransaction
{
    public record DeleteTransactionCommand : IRequest<DeleteTransactionCommand>
    {
        public Guid UserId { get; set; }
        public Guid TransactionId { get; set; }
    }
}