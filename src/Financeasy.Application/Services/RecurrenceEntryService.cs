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

        public async Task<int> GenerateEntries(RecurrenceRule rule, Category category, Guid userId, CancellationToken cancellationToken)
        {
            var countResponse = 0;

            var strategy = _strategyFactory.Create(rule.Frequency);

            DateTime dateParam = DateTime.Today;
            if(rule.EndDate is not null && rule.EndDate <= DateTime.Today)
                dateParam = rule.EndDate.Value;
            
            var dates = strategy.CalculateDates(rule.StartDate, dateParam, rule);

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
                countResponse++;
            }

            return countResponse;
        }
    }
}