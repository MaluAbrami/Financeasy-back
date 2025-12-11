using MediatR;

namespace Financeasy.Application.UseCases.DashboardsCases.GetBalanceEvolution
{
    public class GetBalanceEvolutionHandler : IRequestHandler<GetBalanceEvolution, GetBalanceEvolutionResponse>
    {
        public async Task<GetBalanceEvolutionResponse> Handle(GetBalanceEvolution request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}