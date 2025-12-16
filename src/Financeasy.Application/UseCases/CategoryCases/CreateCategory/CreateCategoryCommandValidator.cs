using FluentValidation;

namespace Financeasy.Application.UseCases.CategoryCases.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nome é obrigatório.");

            RuleFor(x => x.Type).IsInEnum();

            RuleFor(x => x.Recurrence).GreaterThan(0).WithMessage("Recorrência é obrigatório quando é fixo.").When(x => x.IsFixed == true);

            RuleFor(x => x.Recurrence)
            .Null()
            .WithMessage("Recorrência só deve ser informada quando o item é fixo.")
            .When(x => !x.IsFixed);
        }
    }
}