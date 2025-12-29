using Financeasy.Domain.interfaces;
using MediatR;

namespace Financeasy.Application.UseCases.RecurrenceRuleCases.GetRecurrenceRuleById
{
    public class GetRecurrenceRuleByIdHandler : IRequestHandler<GetRecurrenceRuleById, GetRecurrenceRuleByIdResponse>
    {
        private readonly IRecurrenceRuleRepository _recurrenceRepository;

        public GetRecurrenceRuleByIdHandler(IRecurrenceRuleRepository recurrenceRepository)
        {
            _recurrenceRepository = recurrenceRepository;
        }

        public async Task<GetRecurrenceRuleByIdResponse> Handle(GetRecurrenceRuleById request, CancellationToken cancellationToken)
        {
            var recurrenceExists = await _recurrenceRepository.GetByIdAsync(request.Id);
            if(recurrenceExists is null)
                throw new ArgumentException($"Recorrência de id {request.Id} não foi encontrada.");

            return new GetRecurrenceRuleByIdResponse
            {
                Id = recurrenceExists.Id,
                CategoryId = recurrenceExists.CategoryId,
                Frequency = recurrenceExists.Frequency,
                DayOfMonth = recurrenceExists.DayOfMonth,
                DayOfWeek = recurrenceExists.DayOfWeek,
                AdjustmentRule = recurrenceExists.AdjustmentRule,
                StartDate = recurrenceExists.StartDate,
                EndDate = recurrenceExists.EndDate,
                Amount = recurrenceExists.Amount,
                IsActive = recurrenceExists.IsActive
            };
        }
    }
}