using Financeasy.Application.Strategy;
using Financeasy.Domain.Enums;

namespace Financeasy.Application.Factory
{
    public interface IRecurrenceStrategyFactory
    {
        IRecurrenceStrategy Create(Frequency frequency);
    }
}