using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using Financeasy.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Financeasy.Infra.Repository
{
    public class CardInvoiceRepository : BaseRepository<CardInvoice>, ICardInvoiceRepository
    {
        private readonly FinanceasyDbContext _context;

        public CardInvoiceRepository(FinanceasyDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CardInvoice> GetOrCreateAsync(Guid cardId, DateTime closingDate, DateTime dueDate)
        {
            var invoice = await _context.CardInvoices.FirstOrDefaultAsync(
                x => x.CardId == cardId &&
                x.ClosingDate == closingDate &&
                x.DueDate == dueDate
            );

            if(invoice is not null)
                return invoice;

            var newInvoice = new CardInvoice(
                cardId,
                closingDate,
                dueDate,
                0
            );

            await _context.CardInvoices.AddAsync(newInvoice);

            return newInvoice;
        }
    }
}