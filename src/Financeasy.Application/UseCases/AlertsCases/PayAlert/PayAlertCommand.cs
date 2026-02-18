using MediatR;

namespace Financeasy.Application.UseCases.AlertsCases.PayAlert
{
    public record PayAlertCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}