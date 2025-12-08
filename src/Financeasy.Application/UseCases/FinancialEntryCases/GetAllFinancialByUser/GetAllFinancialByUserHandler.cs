using MediatR;

namespace Financeasy.Application.UseCases.FinancialEntryCases.GetAllFinancialByUser
{
    public class GetAllFinancialByUserHandler : IRequestHandler<GetAllFinancialByUser, GetAllFinancialResponseDTO>
    {
        public Task<GetAllFinancialResponseDTO> Handle(GetAllFinancialByUser request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}