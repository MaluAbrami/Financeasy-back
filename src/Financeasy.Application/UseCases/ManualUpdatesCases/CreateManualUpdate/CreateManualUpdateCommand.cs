using MediatR;

namespace Financeasy.Application.UseCases.ManualUpdatesCases.CreateManualUpdate
{
    public record CreateManualUpdateCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
    }
}