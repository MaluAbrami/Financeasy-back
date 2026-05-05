using System.Linq.Expressions;
using Financeasy.Domain.DTO.Pagination;
using Financeasy.Domain.Enums;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using MediatR;

namespace Financeasy.Application.UseCases.TransactionCases.GetAllTransactions
{
    public class GetAllTransactionsHandler : IRequestHandler<GetAllTransactionsQuery, GetAllTransactionResponse>
    {
        private readonly ITransactionDapperRepository _transactionDapper;

        public GetAllTransactionsHandler(ITransactionDapperRepository transactionDapper)
        {
            _transactionDapper = transactionDapper;
        }

        public async Task<GetAllTransactionResponse> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            var orderBy = request.OrderBy switch
            {
                TransactionOrderBy.Date => "date",
                TransactionOrderBy.Amount => "amount",
                _ => "date"
            };

            var getPagedTransactions = await _transactionDapper.GetPagedAsync(
                request.UserId,
                null,
                orderBy,
                request.Direction == SortDirection.Asc,
                request.Pagination.Page,
                request.Pagination.PageSize,
                cancellationToken
            );

            return new GetAllTransactionResponse
            {
                Transactions = getPagedTransactions.List,
                Pagination = new PaginationResponseBase
                {
                    Page = request.Pagination.Page,
                    PageSize = request.Pagination.PageSize,
                    TotalItems = getPagedTransactions.TotalItems,
                    TotalPages = (int)Math.Ceiling(
                        getPagedTransactions.TotalItems /
                        (double)request.Pagination.PageSize
                    )
                }
            };
        }
    }
}