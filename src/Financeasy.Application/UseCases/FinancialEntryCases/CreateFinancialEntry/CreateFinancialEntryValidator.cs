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

            RuleFor(x => x.Data.Amount)
                .NotEmpty().WithMessage("Valor é obrigatório");

            RuleFor(x => x.Data.CategoryId)
                .NotEmpty().WithMessage("Id da Categoria é obrigatória");

            RuleFor(x => x.Data.Date)
                .NotEmpty().WithMessage("Data é obrigatório");
        }
    }
}