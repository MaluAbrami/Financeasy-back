using MediatR;

namespace Financeasy.Application.UseCases.FinancialEntryCases.DeleteFinancialEntry
{
    public class DeleteFinancialEntryHandler : IRequestHandler<DeleteFinancialEntryCommand, DeleteFinancialEntryCommand>
    {
        public Task<DeleteFinancialEntryCommand> Handle(DeleteFinancialEntryCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}