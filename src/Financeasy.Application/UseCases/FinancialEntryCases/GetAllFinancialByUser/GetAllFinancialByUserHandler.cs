using MediatR;

namespace Financeasy.Application.UseCases.FinancialEntryCases.GetAllFinancialByUser
{
    public class GetAllFinancialByUserHandler : IRequestHandler<GetAllFinancialByUser, GetAllFinancialByUserResponse>
    {
        public Task<GetAllFinancialByUserResponse> Handle(GetAllFinancialByUser request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}