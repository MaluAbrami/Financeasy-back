using Financeasy.Domain.DTO.BankAccount;
using MediatR;

namespace Financeasy.Application.UseCases.BankAccount.CreateBankAccount
{
    public record CreateBankAccountCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public CreateBankAccountRequest Request { get; set; }
    }
}