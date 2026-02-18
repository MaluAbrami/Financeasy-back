using MediatR;

namespace Financeasy.Application.UseCases.AlertsCases.DeleteAlert
{
    public class DeleteAlertCommand : IRequest<DeleteAlertCommand>
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}