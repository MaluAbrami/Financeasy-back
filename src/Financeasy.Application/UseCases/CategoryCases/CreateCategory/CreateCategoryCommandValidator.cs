using FluentValidation;

namespace Financeasy.Application.UseCases.CategoryCases.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nome é obrigatório.");

            RuleFor(x => x.Type).IsInEnum();

            RuleFor(x => x.RecurrenceType).IsInEnum();
        }
    }
}