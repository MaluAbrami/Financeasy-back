
using Financeasy.Application.Strategy;
using Financeasy.Domain.Enums;

namespace Financeasy.Application.Factory
{
    public class RecurrenceStrategyFactory : IRecurrenceStrategyFactory
    {
        public IRecurrenceStrategy Create(Frequency frequency)
        {
            return frequency switch
            {
                Frequency.Weekly => new WeeklyRecurrenceStrategy(),
                Frequency.Monthly => new MonthlyRecurrenceStrategy(),
                Frequency.Yearly => new YearlyRecurrenceStrategy(),
                _ => throw new ArgumentException("Frequência inválida")
            };
        }
    }
}