using MediatR;

namespace Financeasy.Application.UseCases.FinancialEntryCases.DeleteFinancialEntry
{
    public record DeleteFinancialEntryCommand : IRequest<DeleteFinancialEntryCommand>
    {
        public Guid FinancialId { get; set; }
        public Guid UserId { get; set; }
    }
}