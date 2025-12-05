using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using Financeasy.Infra.Persistence;

namespace Financeasy.Infra.Repository
{
    public class FinancialEntryRepository : BaseRepository<FinancialEntry>, IFinancialEntryRepository
    {
        private readonly FinanceasyDbContext _context;

        public FinancialEntryRepository(FinanceasyDbContext context) : base(context)
        {
            _context = context;
        }
    }
}