using Financeasy.Domain.models;

namespace Financeasy.Application.Services
{
    public interface IRecurrenceEntryService
    {
        public Task GenerateEntries(RecurrenceRule rule, Category category, Guid userId, CancellationToken cancellationToken);
    }
}