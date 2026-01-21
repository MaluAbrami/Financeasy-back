using Financeasy.Domain.DTO.BankAccount;
using Financeasy.Domain.DTO.Pagination;
using Financeasy.Domain.Enums;
using MediatR;

namespace Financeasy.Application.UseCases.BankAccountCases.GetAllBanksAccounts
{
    public record GetAllBanksAccountsResponse()
    {
        public List<GetBankAccountDTO > BanksAccounts { get; set; }
        public PaginationResponseBase Pagination { get; set; }
    }

    public record GetAllBanksAccountsQuery : IRequest<GetAllBanksAccountsResponse>
    {
        public Guid UserId { get; set; }
        public PaginationRequestBase Pagination { get; set; }
        public BankAccountOrderBy OrderBy { get; set; }
        public SortDirection Direction { get; set; }
    }
}