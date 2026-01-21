using System.Linq.Expressions;
using Financeasy.Domain.DTO.BankAccount;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using Financeasy.Infra.Persistence;

namespace Financeasy.Infra.Repository
{
    public class BankAccountRepository : BaseRepository<BankAccount>, IBankAccountRepository
    {
        private readonly FinanceasyDbContext _dbContext;

        public BankAccountRepository(FinanceasyDbContext context) : base(context)
        {
            _dbContext = context;
        }
    }
}