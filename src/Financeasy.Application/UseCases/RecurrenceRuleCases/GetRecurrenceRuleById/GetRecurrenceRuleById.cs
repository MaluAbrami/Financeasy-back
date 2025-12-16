using Financeasy.Domain.Enums;
using MediatR;

namespace Financeasy.Application.UseCases.RecurrenceRuleCases.GetRecurrenceRuleById
{
    public record GetRecurrenceRuleByIdResponse()
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Frequency Frequency { get; set; }
        public int? DayOfMonth { get; set; }
        public int? DayOfWeek { get; set; }
        public AdjustmentRule AdjustmentRule { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
    }

    public record GetRecurrenceRuleById : IRequest<GetRecurrenceRuleByIdResponse>
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}