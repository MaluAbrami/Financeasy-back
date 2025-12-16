using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Financeasy.Domain.models;

namespace Financeasy.Application.Strategy
{
    public class WeeklyRecurrenceStrategy : IRecurrenceStrategy
    {
        public IEnumerable<DateTime> CalculateDates(DateTime startDate, DateTime endDate, RecurrenceRule rule)
        {
            var dates = new List<DateTime>();

            if (rule.DayOfWeek is null)
                throw new ArgumentException("DayOfWeek é obrigatório para recorrência semanal.");

            int daysUntilTarget =
                ((int)rule.DayOfWeek.Value - (int)startDate.DayOfWeek + 7) % 7;

            var current = startDate.AddDays(daysUntilTarget);
            
            while (current <= endDate)
            {
                dates.Add(current);
                current = current.AddDays(7);
            }

            return dates;
        }
    }
}