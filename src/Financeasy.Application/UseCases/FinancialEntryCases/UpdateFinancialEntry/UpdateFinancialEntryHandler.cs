using Financeasy.Domain.DTO;
using MediatR;

namespace Financeasy.Application.UseCases.FinancialEntryCases.UpdateFinancialEntry
{
    public class UpdateFinancialEntryHandler : IRequestHandler<UpdateFinancialEntryCommand, FinancialResponseDTO>
    {
        public Task<FinancialResponseDTO> Handle(UpdateFinancialEntryCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}