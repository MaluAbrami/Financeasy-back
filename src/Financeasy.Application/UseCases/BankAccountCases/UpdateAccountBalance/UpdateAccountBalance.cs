using MediatR;

namespace Financeasy.Application.UseCases.BankAccountCases.UpdateAccountBalance
{
    public record UpdateAccountBalanceResponse()
    {
        public Guid Id { get; set; }
        public string Bank { get; set; }
        public decimal Balance { get; set; }
    }

    public record UpdateAccountBalance : IRequest<UpdateAccountBalanceResponse>
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public decimal Balance { get; set; }
    }
}