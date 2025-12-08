using Financeasy.Domain.Enums;
using FluentValidation;

namespace Financeasy.Application.UseCases.FinancialEntryCases.CreateFinancialEntry
{
    public class CreateFinancialEntryValidator : AbstractValidator<CreateFinancialEntryCommand>
    {
        public CreateFinancialEntryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Id do usuário é obrigatório");

            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Valor é obrigatório");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Categoria é obrigatória");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Data é obrigatório");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Tipo é obrigatório")
                .IsInEnum().WithMessage("Tipo de entrada inválido");
        }
    }
}