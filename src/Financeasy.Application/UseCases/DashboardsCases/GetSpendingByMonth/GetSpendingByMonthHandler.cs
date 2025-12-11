using MediatR;

namespace Financeasy.Application.UseCases.DashboardsCases.GetSpendingByMonth
{
    public class GetSpendingByMonthHandler : IRequestHandler<GetSpendingByMonth, GetSpendingByMonthResponse>
    {
        public async Task<GetSpendingByMonthResponse> Handle(GetSpendingByMonth request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}