using Financeasy.Application.UseCases.RecurrenceRuleCases.GetRecurrenceRuleById;
using Financeasy.Domain.DTO;
using Financeasy.Domain.interfaces;
using MediatR;

namespace Financeasy.Application.UseCases.RecurrenceRuleCases.GetAllRecurrencesByCategoryId
{
    public class GetAllRecurrencesByCategoryIdHandler : IRequestHandler<GetAllRecurrenceByCategoryId, GetAllRecurrenceByCategoryIdResponse>
    {
        private readonly IRecurrenceRuleRepository _recurrenceRepository;

        public GetAllRecurrencesByCategoryIdHandler(IRecurrenceRuleRepository recurrenceRepository)
        {
            _recurrenceRepository = recurrenceRepository;
        }

        public async Task<GetAllRecurrenceByCategoryIdResponse> Handle(GetAllRecurrenceByCategoryId request, CancellationToken cancellationToken)
        {
            var recurrences = await _recurrenceRepository.FindAsync(x => x.CategoryId == request.CategoryId && x.UserId == request.UserId);

            List<RecurrenceResponseDTO> listResponse = [];

            foreach (var recurrence in recurrences)
            {
                var recurrenceResponse = new RecurrenceResponseDTO
                {
                    Id = recurrence.Id,
                    Frequency = recurrence.Frequency,
                    DayOfMonth = recurrence.DayOfMonth,
                    DayOfWeek = recurrence.DayOfWeek,
                    AdjustmentRule = recurrence.AdjustmentRule,
                    StartDate = recurrence.StartDate,
                    EndDate = recurrence.EndDate,
                    Amount = recurrence.Amount,
                    IsActive = recurrence.IsActive
                };

                listResponse.Add(recurrenceResponse);
            }

            return new GetAllRecurrenceByCategoryIdResponse { Recurrences = listResponse };
        }
    }
}