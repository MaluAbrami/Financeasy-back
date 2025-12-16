using Financeasy.Domain.DTO;
using MediatR;

namespace Financeasy.Application.UseCases.RecurrenceRuleCases.CreateRecurrenceRule
{
    public record CreateRecurrenceRuleCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public required CreateRecurrenceRequestDTO Recurrence { get; set; }
    }
}