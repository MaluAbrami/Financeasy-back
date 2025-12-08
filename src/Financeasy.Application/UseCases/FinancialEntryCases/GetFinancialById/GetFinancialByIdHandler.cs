using Financeasy.Domain.DTO;
using MediatR;

namespace Financeasy.Application.UseCases.FinancialEntryCases.GetFinancialById
{
    public class GetFinancialByIdHandler : IRequestHandler<GetFinancialById, FinancialResponseDTO>
    {
        public Task<FinancialResponseDTO> Handle(GetFinancialById request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}