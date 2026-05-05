using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using Financeasy.Infra.Persistence;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace Financeasy.Infra.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly FinanceasyDbContext _context;

        public CategoryRepository(FinanceasyDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Category?> GetByIdAndUserId(Guid id, Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Categorys.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        }
    }
}