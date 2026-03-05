using Financeasy.Domain.models;

namespace Financeasy.Domain.interfaces.Services
{
    public interface IInvoiceService
    {
        public  Task<CardInvoice> GetOrGenerateInvoice(
            Card card, DateTime purchaseDate, int number, CancellationToken cancellationToken
        );
    }
}