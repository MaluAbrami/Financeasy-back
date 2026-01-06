using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using Financeasy.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Financeasy.Infra.Repository
{
    public class UpdateRepository : BaseRepository<Update>, IUpdateRepository
    {
        private readonly FinanceasyDbContext _context;

        public UpdateRepository(FinanceasyDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Update?> GetLastAsync()
        {
            return await _context.Updates.OrderByDescending(x => x.UpdateDate).FirstOrDefaultAsync();
        }
    }
}