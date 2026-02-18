using Financeasy.Domain.Enums;
using MediatR;

namespace Financeasy.Application.UseCases.AlertsCases.CreateAlert
{
    public record CreateAlertCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public RecurrenceType RecurrenceType { get; set; }
        public DateTime DueDate { get; set; }
        public decimal ExpectedAmount { get; set; }
    }
}