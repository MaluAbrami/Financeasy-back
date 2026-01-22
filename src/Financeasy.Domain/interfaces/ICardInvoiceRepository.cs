using Financeasy.Domain.models;

namespace Financeasy.Domain.interfaces
{
    public interface ICardInvoiceRepository : IBaseRepository<CardInvoice>
    {
        Task<CardInvoice> GetOrCreateAsync(Guid cardId, DateTime closingDate, DateTime dueDate, CancellationToken cancellationToken);
    }
}