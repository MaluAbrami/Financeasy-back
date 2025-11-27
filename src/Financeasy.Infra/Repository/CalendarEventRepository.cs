using Financeasy.Domain.Interfaces;
using Financeasy.Domain.Models;
using Financeasy.Infra.Data;

namespace Financeasy.Infra.Repository
{
    public class CalendarEventRepository : BaseRepository<CalendarEvent>, ICalendarEventRepository
    {
        public CalendarEventRepository(FinanceasyDbContext dbContext) : base(dbContext)
        {
        }
    }
}