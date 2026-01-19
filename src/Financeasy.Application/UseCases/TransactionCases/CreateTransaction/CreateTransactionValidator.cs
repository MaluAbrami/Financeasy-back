using Financeasy.Application.UseCases.TransactionCases.CreateTransaction;
using FluentValidation;

namespace Financeasy.Application.UseCases.TransactionCases.CreateTransaction
{
    public class CreateTransactionValidator : AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionValidator()
        {
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Categoria é obrigatória");

            RuleFor(x => x.BankAccountId).NotEmpty().WithMessage("Banco é obrigatório");

            RuleFor(x => x.PaymentMethod).IsInEnum().WithMessage("Método de pagamento inválido");

            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Valor não pode ser menor ou igual a zero");
        }
    }
}