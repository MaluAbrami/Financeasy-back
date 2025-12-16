using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using Financeasy.Infra.Persistence;

namespace Financeasy.Infra.Repository
{
    public class RecurrenceRuleRepository : BaseRepository<RecurrenceRule>, IRecurrenceRuleRepository
    {
        public RecurrenceRuleRepository(FinanceasyDbContext context) : base(context)
        {
        }
    }
}