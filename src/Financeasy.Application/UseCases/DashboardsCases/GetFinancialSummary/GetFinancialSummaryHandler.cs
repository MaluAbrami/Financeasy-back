using MediatR;

namespace Financeasy.Application.UseCases.DashboardsCases.GetFinancialSummary
{
    public class GetFinancialSummaryHandler : IRequestHandler<GetFinancialSummary, GetFinancialSummaryResponse>
    {
        public async Task<GetFinancialSummaryResponse> Handle(GetFinancialSummary request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}