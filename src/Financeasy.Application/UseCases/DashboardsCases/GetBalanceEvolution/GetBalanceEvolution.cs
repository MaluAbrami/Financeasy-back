using MediatR;

namespace Financeasy.Application.UseCases.DashboardsCases.GetBalanceEvolution
{
    public record GetBalanceEvolutionResponse()
    {
        
    }

    public record GetBalanceEvolution : IRequest<GetBalanceEvolutionResponse>
    {
        
    }
}