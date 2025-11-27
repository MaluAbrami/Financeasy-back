using Financeasy.Domain.Interfaces;
using Financeasy.Domain.Models;
using Financeasy.Infra.Data;

namespace Financeasy.Infra.Repository
{
    public class MonthlySummaryRepository : BaseRepository<MonthlySummary>, IMonthlySummaryRepository
    {
        public MonthlySummaryRepository(FinanceasyDbContext dbContext) : base(dbContext)
        {
        }
    }
}