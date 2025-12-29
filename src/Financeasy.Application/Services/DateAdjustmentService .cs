using Financeasy.Domain.Enums;

namespace Financeasy.Application.Services
{
    public class DateAdjustmentService : IDateAdjustmentService
    {
        public DateTime Adjust(DateTime date, AdjustmentRule rule)
        {
            return rule switch
            {
                AdjustmentRule.Exact => date,
                AdjustmentRule.FifthBusinessDay => FifthBusinessDay(date),
                AdjustmentRule.LastDayOfMonth => LastDayOfMonth(date),
                AdjustmentRule.LastBusinessDay => LastBusinessDay(date),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private static DateTime LastBusinessDay(DateTime date)
        {
            var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            var d = new DateTime(date.Year, date.Month, daysInMonth);

            while(d.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
                d.AddDays(-1);
            
            return d;
        }

        private static DateTime LastDayOfMonth(DateTime date)
        {
            var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            return new DateTime(date.Year, date.Month, daysInMonth);
        }

        private static DateTime FifthBusinessDay(DateTime date)
        {
            var d = new DateTime(date.Year, date.Month, 1);
            int count = 0;

            while (true)
            {
                if (d.DayOfWeek is not (DayOfWeek.Saturday or DayOfWeek.Sunday))
                    count++;

                if (count == 5)
                    return d;

                d = d.AddDays(1);
            }
        }
    }
}