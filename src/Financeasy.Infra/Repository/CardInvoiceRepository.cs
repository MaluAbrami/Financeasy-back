using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using Financeasy.Infra.Persistence;

namespace Financeasy.Infra.Repository
{
    public class CardInvoiceRepository : BaseRepository<CardInvoice>, ICardInvoiceRepository
    {
        public CardInvoiceRepository(FinanceasyDbContext context) : base(context)
        {
        }
    }
}