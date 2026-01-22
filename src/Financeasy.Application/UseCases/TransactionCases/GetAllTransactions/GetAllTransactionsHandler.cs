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
        private readonly ITransactionRepository _transactionRepository;

        public GetAllTransactionsHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<GetAllTransactionResponse> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Transaction, object>> expression = 
                request.OrderBy switch
                {
                    TransactionOrderBy.Date => x => x.Date,
                    TransactionOrderBy.Amount => x => x.Amount,
                    _ => x => x.Date
                };

            var getPagedTransactions = await _transactionRepository.GetPagedWithRelationsAsync(
                x => x.UserId == request.UserId, 
                expression,
                request.Direction == SortDirection.Asc
                ? true
                : false,
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
                        getPagedTransactions.TotalItems / (double)request.Pagination.PageSize
                    )
                }
            };
        }
    }
}