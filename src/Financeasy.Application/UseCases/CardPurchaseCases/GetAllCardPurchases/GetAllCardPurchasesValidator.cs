using FluentValidation;

namespace Financeasy.Application.UseCases.CardPurchaseCases.GetAllCardPurchases
{
    public class GetAllCardPurchasesValidator : AbstractValidator<GetAllCardPurchasesQuery>
    {
        public GetAllCardPurchasesValidator()
        {
            RuleFor(x => x.OrderBy).IsInEnum().WithMessage("Método de ordenação inválido");

            RuleFor(x => x.Direction).IsInEnum().WithMessage("Ordem de listagem inválido");
        }
    }
}