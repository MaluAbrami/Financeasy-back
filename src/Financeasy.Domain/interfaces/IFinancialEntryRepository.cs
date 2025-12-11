using Financeasy.Domain.Enums;
using Financeasy.Domain.models;

namespace Financeasy.Domain.interfaces
{
    public interface IFinancialEntryRepository : IBaseRepository<FinancialEntry>
    {
        Task<decimal> GetTotalAmountByType(EntryType type, Guid userId);
        Task<decimal> GetTotalExpenseByCategory(string category, Guid userId);
        Task<decimal> GetTotalExpenseByMonth(int year, int month, Guid userId);
    }
}