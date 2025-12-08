using FluentValidation;

namespace Financeasy.Application.UseCases.FinancialEntryCases.UpdateFinancialEntry
{
    public class UpdateFinancialEntryValidator : AbstractValidator<UpdateFinancialEntryCommand>
    {
        public UpdateFinancialEntryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id é obrigatório");

            RuleFor(x => x.Amount)
                .LessThan(0).WithMessage("O valor não pode ser negativo");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Tipo de lançamento inválido");
        }
    }
}