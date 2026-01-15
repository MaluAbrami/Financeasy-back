using Financeasy.Domain.DTO.BankAccount;
using MediatR;

namespace Financeasy.Application.UseCases.BankAccountCases.CreateBankAccount
{
    public record CreateBankAccountCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string Bank { get; set; }
        public decimal Balance { get; set; }
    }
}