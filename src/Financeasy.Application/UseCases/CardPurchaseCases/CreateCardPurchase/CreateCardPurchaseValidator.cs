using FluentValidation;

namespace Financeasy.Application.UseCases.CardPurchaseCases.CreateCardPurchase
{
    public class CreateCardPurchaseValidator : AbstractValidator<CreateCardPurchaseCommand>
    {
        public CreateCardPurchaseValidator()
        {
            RuleFor(x => x.TotalAmount).GreaterThan(0).WithMessage("O valor total não pode ser menor ou igual a zero.");
        }
    }
}