using MediatR;

namespace Financeasy.Application.UseCases.DashboardsCases.GetSpendingByCategory
{
    public class GetSpendingByCategoryHandler : IRequestHandler<GetSpendingByCategory, GetSpendingByCategoryResponse>
    {
        public async Task<GetSpendingByCategoryResponse> Handle(GetSpendingByCategory request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}