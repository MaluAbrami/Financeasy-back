using Financeasy.Application.UseCases.RecurrenceRuleCases.GetRecurrenceRuleById;
using Financeasy.Domain.DTO;
using MediatR;

namespace Financeasy.Application.UseCases.RecurrenceRuleCases.GetAllRecurrencesByCategoryId
{
    public record GetAllRecurrenceByCategoryIdResponse()
    {
        public List<RecurrenceResponseDTO> Recurrences { get; set; }
    }

    public record GetAllRecurrenceByCategoryId : IRequest<GetAllRecurrenceByCategoryIdResponse>
    {
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
    }
}