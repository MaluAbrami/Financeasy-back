using Financeasy.Domain.Interfaces;
using Financeasy.Domain.Models;
using Financeasy.Infra.Data;

namespace Financeasy.Infra.Repository
{
    public class RecurringTransactionRepository : BaseRepository<RecurringTransaction>, IRecurringTransactionRepository
    {
        public RecurringTransactionRepository(FinanceasyDbContext dbContext) : base(dbContext)
        {
        }
    }
}