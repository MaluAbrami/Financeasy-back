using MediatR;

namespace Financeasy.Application.UseCases.FinancialEntryCases.GetAllFinancialByUser
{
    public class GetAllFinancialByUserHandler : IRequestHandler<GetAllFinancialByUser, GetAllFinancialResponse>
    {
        public async Task<GetAllFinancialResponse> Handle(GetAllFinancialByUser request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}