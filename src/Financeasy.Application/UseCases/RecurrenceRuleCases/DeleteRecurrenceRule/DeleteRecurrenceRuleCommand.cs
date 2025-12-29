using MediatR;

namespace Financeasy.Application.UseCases.RecurrenceRuleCases.DeleteRecurrenceRule
{
    public record DeleteRecurrenceRuleCommand : IRequest<DeleteRecurrenceRuleCommand>
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}