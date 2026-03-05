using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;

namespace Financeasy.Domain.Services
{
    public class CardService
    {
        private readonly ICardInvoiceDapperRepository _invoiceDapperRepository;

        public CardService(
            ICardInvoiceDapperRepository invoiceDapperRepository
        )
        {
            _invoiceDapperRepository = invoiceDapperRepository;
        }

        public async Task<bool> CheckAvailableLimit(decimal cardCreditLimit, Guid cardId, CancellationToken cancellationToken)
        {
            var totalAmountExpense = await _invoiceDapperRepository.GetTotalAmountUnpaidByCardId(cardId, cancellationToken);

            if(cardCreditLimit - totalAmountExpense < 0)
                return false;
                
            return true;
        }
    }
}