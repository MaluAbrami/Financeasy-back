using Financeasy.Domain.models;

namespace Financeasy.Application.Services
{
    public interface IRecurrenceEntryService
    {
        public Task GenerateEntries(RecurrenceRule rule, Category category, Guid userId, CancellationToken cancellationToken);
        public Task<int> GenerateEntriesInManualUpdate(List<RecurrenceRule> rules, Guid userId, DateTime startDate, CancellationToken cancellationToken);
    }
}