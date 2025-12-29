using FluentValidation;

namespace Financeasy.Application.UseCases.CategoryCases.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nome é obrigatório.");

            RuleFor(x => x.Type).IsInEnum();

            RuleFor(x => x)
                .Must(x => x.IsFixed ? x.Recurrence != null : x.Recurrence == null)
                .WithMessage("A recorrência só deve ser informada para categorias fixas.");
        }
    }
}