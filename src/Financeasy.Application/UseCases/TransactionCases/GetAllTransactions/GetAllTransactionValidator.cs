using FluentValidation;

namespace Financeasy.Application.UseCases.TransactionCases.GetAllTransactions
{
    public class GetAllTransactionValidator : AbstractValidator<GetAllTransactionsQuery>
    {
        public GetAllTransactionValidator()
        {
            RuleFor(x => x.OrderBy).IsInEnum().WithMessage("Método de ordenação inválido");

            RuleFor(x => x.Direction).IsInEnum().WithMessage("Ordem de listagem inválida");
        }
    }
}