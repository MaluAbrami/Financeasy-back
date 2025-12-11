using MediatR;

namespace Financeasy.Application.UseCases.DashboardsCases.GetSpendingByMonth
{
    public record GetSpendingByMonthResponse()
    {
        
    }

    public record GetSpendingByMonth : IRequest<GetSpendingByMonthResponse>
    {
        
    }
}