using MediatR;

namespace Financeasy.Application.UseCases.FinancialEntryCases.DeleteFinancialEntry
{
    public record DeleteFinancialEntryCommand : IRequest<DeleteFinancialEntryCommand>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}