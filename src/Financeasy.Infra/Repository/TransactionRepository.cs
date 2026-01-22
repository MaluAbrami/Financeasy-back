using System.Linq.Expressions;
using Financeasy.Domain.DTO.Pagination;
using Financeasy.Domain.DTO.Transaction;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using Financeasy.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Financeasy.Infra.Repository
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        private readonly FinanceasyDbContext _dbContext;

        public TransactionRepository(FinanceasyDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<Transaction?> GetTransactionWithCategoryAndBank(Guid transactionId)
        {
            return await _dbContext.Transactions
                .Include(t => t.Category)
                .Include(t => t.BankAccount)
                .FirstOrDefaultAsync(t => t.Id == transactionId);
        }

        public async Task<GetPagedTransResponseDTO> GetPagedWithRelationsAsync(    
            Expression<Func<Transaction, bool>> predicate,
            Expression<Func<Transaction, object>> orderBy,
            bool ascending,
            int page,
            int pageSize,
            CancellationToken cancellationToken)
        {
            IQueryable<Transaction> query = _dbContext.Transactions;

            if ( predicate is not null)
                query = query.Where(predicate);

            var totalItems = await query.CountAsync();

            query = ascending
                ? query.OrderBy(orderBy)
                : query.OrderByDescending(orderBy);

            return new GetPagedTransResponseDTO
            {   
                List = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => new GetTransactionResponseDTO
                    {
                        Id = x.Id,
                        BankAccountName = x.BankAccount!.Bank,
                        CategoryName = x.Category.Name,
                        PaymentMethod = x.PaymentMethod,
                        Amount = x.Amount,
                        Date = x.Date,
                        Description = x.Description
                    })
                    .ToListAsync(),
                TotalItems = totalItems
            };
        }
    }
}