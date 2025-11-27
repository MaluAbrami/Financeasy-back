using Financeasy.Domain.Interfaces;
using Financeasy.Domain.Models;
using Financeasy.Infra.Data;

namespace Financeasy.Infra.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(FinanceasyDbContext dbContext) : base(dbContext)
        {
        }
    }
}