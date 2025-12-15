using Financeasy.Domain.Enums;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using Financeasy.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Financeasy.Infra.Repository
{
    public class FinancialEntryRepository : BaseRepository<FinancialEntry>, IFinancialEntryRepository
    {
        private readonly FinanceasyDbContext _context;

        public FinancialEntryRepository(FinanceasyDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<decimal> GetTotalAmountByType(EntryType type, Guid userId)
        {
            return await _context.FinancialEntry
                .Where(f => f.Type == type && f.UserId == userId)
                .SumAsync(f => f.Amount);
        }

        public async Task<decimal> GetTotalAmountByTypeAndByMonth(EntryType type, Guid userId, int year, int month)
        {
            return await _context.FinancialEntry
                .Where(f => f.Type == type && 
                    f.UserId == userId && 
                    f.Date.Year == year &&
                    f.Date.Month == month)
                .SumAsync(f => f.Amount);
        }

        public async Task<decimal> GetTotalExpenseByCategory(string category, Guid userId)
        {
            return await _context.FinancialEntry
                .Where(f => f.Category == category && f.UserId == userId)
                .SumAsync(f => f.Amount);
        }

        public async Task<decimal> GetTotalExpenseByMonth(int year, int month, Guid userId)
        {
            return await _context.FinancialEntry
                .Where(f => f.Date.Year == year && f.Date.Month == month && f.UserId == userId)
                .SumAsync(f => f.Amount);
        }
    }
}