using MediatR;

namespace Financeasy.Application.UseCases.DashboardsCases.GetFinancialSummary
{
    public record GetFinancialSummaryResponse()
    {
        
    }

    public record GetFinancialSummary : IRequest<GetFinancialSummaryResponse >
    {
        
    }
}