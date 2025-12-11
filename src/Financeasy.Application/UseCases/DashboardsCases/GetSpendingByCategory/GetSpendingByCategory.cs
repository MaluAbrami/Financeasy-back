using MediatR;

namespace Financeasy.Application.UseCases.DashboardsCases.GetSpedingByCategory
{
    public record GetSpendingByCategoryResponse()
    {
        
    }

    public record GetSpendingByCategory : IRequest<GetSpendingByCategoryResponse>
    {
        
    }
}