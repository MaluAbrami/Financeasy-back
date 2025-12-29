using Financeasy.Domain.models;

namespace Financeasy.Application.Strategy
{
    public class MonthlyRecurrenceStrategy : IRecurrenceStrategy
    {
        public IEnumerable<DateTime> CalculateDates(DateTime startDate, DateTime endDate, RecurrenceRule rule)
        {
            List<DateTime> dates = [];
            var current = startDate;

            while(current <= endDate)
            {
                var daysInMonth = DateTime.DaysInMonth(current.Year, current.Month);
                var day = Math.Min(rule.DayOfMonth!.Value, daysInMonth);

                dates.Add(new DateTime(current.Year, current.Month, day));

                current = current.AddMonths(1);
            }

            return dates;
        }
    }
}