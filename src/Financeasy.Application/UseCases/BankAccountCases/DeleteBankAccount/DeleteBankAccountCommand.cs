using MediatR;

namespace Financeasy.Application.UseCases.BankAccountCases.DeleteBankAccount
{
    public record DeleteBankAccountCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public Guid BankAccountId { get; set; }
    }
}