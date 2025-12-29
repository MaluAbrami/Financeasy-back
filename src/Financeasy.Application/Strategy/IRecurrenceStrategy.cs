using Financeasy.Domain.models;

namespace Financeasy.Application.Strategy
{
    public interface IRecurrenceStrategy
    {
        IEnumerable<DateTime> CalculateDates(
        DateTime startDate,
        DateTime endDate,
        RecurrenceRule rule);
    }
}