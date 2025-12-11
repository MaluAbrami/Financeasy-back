using MediatR;

namespace Financeasy.Application.UseCases.DashboardsCases.GetSpendingByCategory
{
    public record GetSpendingByCategoryResponse()
    {
        
    }

    public record GetSpendingByCategory : IRequest<GetSpendingByCategoryResponse>
    {
        
    }
}