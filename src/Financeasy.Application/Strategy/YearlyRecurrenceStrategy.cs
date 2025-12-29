using Financeasy.Domain.models;

namespace Financeasy.Application.Strategy
{
    public class YearlyRecurrenceStrategy : IRecurrenceStrategy
    {
        public IEnumerable<DateTime> CalculateDates(DateTime startDate, DateTime endDate, RecurrenceRule rule)
        {
            var dates = new List<DateTime>();

            var year = startDate.Year;
            var targetMonth = startDate.Month;
            var targetDay = startDate.Day;

            while (true)
            {
                if (year > endDate.Year)
                    break;

                var daysInMonth = DateTime.DaysInMonth(year, targetMonth);
                var day = Math.Min(targetDay, daysInMonth);

                var date = new DateTime(year, targetMonth, day);

                if (date >= startDate && date <= endDate)
                    dates.Add(date);

                year++;
            }

            return dates;
        }
    }
}