using FluentValidation;

namespace Financeasy.Application.UseCases.FinancialEntryCases.UpdateFinancialEntry
{
    public class UpdateFinancialEntryValidator : AbstractValidator<UpdateFinancialEntryCommand>
    {
        public UpdateFinancialEntryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id é obrigatório");

            RuleFor(x => x.Data.Amount)
                .GreaterThanOrEqualTo(0).WithMessage("O valor não pode ser negativo");

            RuleFor(x => x.Data.Type)
                .IsInEnum().WithMessage("Tipo de lançamento inválido");
        }
    }
}