using Financeasy.Domain.Models;

namespace Financeasy.Domain.Interfaces
{
    public interface ICalendarEventRepository : IBaseRepository<CalendarEvent>
    {
        public Task<CalendarEvent> GetCalendarEventWithTransactionById(Guid id);
    }
}