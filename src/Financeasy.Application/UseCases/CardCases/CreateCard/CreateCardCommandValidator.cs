using FluentValidation;

namespace Financeasy.Application.UseCases.CardCases.CreateCard
{
    public class CreateCardCommandValidator : AbstractValidator<CreateCardCommand>
    {
        public CreateCardCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nome do cartão de crédito é obrigatório");

            RuleFor(x => x.CreditLimit).GreaterThan(0).WithMessage("O limite de crédito do cartão não pode ser menor que zero");
        }
    }
}