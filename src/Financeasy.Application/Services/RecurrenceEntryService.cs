using Financeasy.Application.Factory;
using Financeasy.Domain.Enums;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;

namespace Financeasy.Application.Services
{
    public class RecurrenceEntryService : IRecurrenceEntryService
    {
        private readonly IRecurrenceStrategyFactory _strategyFactory;
        private readonly IFinancialEntryRepository _financialEntryRepository;

        public RecurrenceEntryService(IRecurrenceStrategyFactory strategyFactory, IFinancialEntryRepository financialEntryRepository)
        {
            _strategyFactory = strategyFactory;
            _financialEntryRepository = financialEntryRepository;
        }       

        public async Task GenerateEntries(RecurrenceRule rule, Category category, Guid userId, CancellationToken cancellationToken)
        {
            var strategy = _strategyFactory.Create(rule.Frequency);

            var dates = strategy.CalculateDates(rule.StartDate, DateTime.Today, rule);

            foreach(var date in dates)
            {
                var entry = new FinancialEntry(
                    userId,
                    rule.Amount,
                    "lançamento automático",
                    date,
                    category,
                    SourceType.Automatic
                );

                await _financialEntryRepository.AddAsync(entry);
            }
        }
    }
}