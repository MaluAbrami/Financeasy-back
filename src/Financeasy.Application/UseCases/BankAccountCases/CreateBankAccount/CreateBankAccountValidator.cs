using FluentValidation;

namespace Financeasy.Application.UseCases.BankAccountCases.CreateBankAccount
{
    public class CreateBankAccountValidator : AbstractValidator<CreateBankAccountCommand>
    {
        public CreateBankAccountValidator()
        {
            RuleFor(x => x.Bank).NotEmpty().WithMessage("Nome do banco é obrigatório");
        }
    }
}