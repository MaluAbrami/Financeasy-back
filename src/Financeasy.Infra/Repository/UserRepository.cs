using System.Linq.Expressions;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using Financeasy.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Financeasy.Infra.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly FinanceasyDbContext _context;

        public UserRepository(FinanceasyDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}