using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;

namespace Financeasy.Application.Services
{
    public class UpdateExecutionService : IUpdateExecutionService
    {
        private readonly IUpdateRepository _updateRepository;
        private readonly IRecurrenceRuleRepository _recurrenceRuleRepository;
        private readonly IRecurrenceEntryService _recurrenceService;

        public UpdateExecutionService(
            IUpdateRepository updateRepository,
            IRecurrenceRuleRepository recurrenceRuleRepository,
            IRecurrenceEntryService recurrenceService)
        {
            _updateRepository = updateRepository;
            _recurrenceRuleRepository = recurrenceRuleRepository;
            _recurrenceService = recurrenceService;
        }

        public async Task<Update> ExecuteAsync(
            Guid userId,
            DateTime executionDate,
            CancellationToken ct)
        {
            var updates = await _updateRepository.GetAllAsync();
            var lastUpdateDate = new DateTime();

            if(updates.Count > 0)
                lastUpdateDate = updates.Last().UpdateDate;

            if (lastUpdateDate.Date == executionDate.Date)
                throw new InvalidOperationException("A atualização diária já foi realizada.");

            var recurrences = await _recurrenceRuleRepository.FindAsync(
                x => (x.EndDate == null || x.EndDate >= executionDate)
                     && x.UserId == userId
            );

            var totalGenerated = recurrences.Any()
                ? await _recurrenceService.GenerateEntriesInManualUpdate(
                    recurrences, userId, lastUpdateDate, ct)
                : 0;

            return new Update(userId, executionDate, totalGenerated);
        }
    }

}