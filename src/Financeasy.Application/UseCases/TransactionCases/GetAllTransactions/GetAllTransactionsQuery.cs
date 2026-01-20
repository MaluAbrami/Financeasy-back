using Financeasy.Domain.DTO.Pagination;
using Financeasy.Domain.DTO.Transaction;
using Financeasy.Domain.Enums;
using MediatR;

namespace Financeasy.Application.UseCases.TransactionCases.GetAllTransactions
{
    public record GetAllTransactionResponse()
    {
        public List<GetTransactionResponseDTO> Transactions { get; set; }
        public PaginationResponseBase Pagination { get; set; }
    }

    public record GetAllTransactionsQuery : IRequest<GetAllTransactionResponse>
    {
        public Guid UserId { get; set; }
        public PaginationRequestBase Pagination { get; set; }
        public TransactionOrderBy OrderBy { get; set; } = TransactionOrderBy.Date;
        public SortDirection Direction { get; set; } = SortDirection.Asc;
    }
}