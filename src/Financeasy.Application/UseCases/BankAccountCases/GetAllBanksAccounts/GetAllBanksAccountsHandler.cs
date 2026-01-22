using System.Linq.Expressions;
using Financeasy.Domain.DTO.BankAccount;
using Financeasy.Domain.DTO.Pagination;
using Financeasy.Domain.Enums;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using MediatR;

namespace Financeasy.Application.UseCases.BankAccountCases.GetAllBanksAccounts
{
    public class GetAllBanksAccountsHandler : IRequestHandler<GetAllBanksAccountsQuery, GetAllBanksAccountsResponse>
    {
        private readonly IBankAccountRepository _bankAccountRepository;

        public GetAllBanksAccountsHandler(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task<GetAllBanksAccountsResponse> Handle(GetAllBanksAccountsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<BankAccount, object>> expression =
                request.OrderBy switch
                {
                    BankAccountOrderBy.Bank => x => x.Bank,
                    BankAccountOrderBy.Balance => x => x.Balance,
                    _ => x => x.Balance
                };

            var banksAccounts = await _bankAccountRepository.GetPagedAsync(
                x => x.UserId == request.UserId,
                expression,
                request.Direction == SortDirection.Asc
                ? true
                : false,
                request.Pagination.Page,
                request.Pagination.PageSize,
                cancellationToken
            );

            List<GetBankAccountDTO> list = [];
            foreach (var bankAccount in banksAccounts.List)
            {
                var bankAccountDto = new GetBankAccountDTO
                {
                    Id = bankAccount.Id,
                    Bank = bankAccount.Bank,
                    Balance = bankAccount.Balance
                };

                list.Add(bankAccountDto);
            }

            return new GetAllBanksAccountsResponse
            {
                BanksAccounts = list,
                Pagination = new PaginationResponseBase
                {
                    Page = request.Pagination.Page,
                    PageSize = request.Pagination.PageSize,
                    TotalItems = banksAccounts.TotalItems,
                    TotalPages = (int)Math.Ceiling(
                        banksAccounts.TotalItems / (double)request.Pagination.PageSize
                    )
                }
            };
        }
    }
}