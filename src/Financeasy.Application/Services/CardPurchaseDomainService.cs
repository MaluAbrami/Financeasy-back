using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;

namespace Financeasy.Application.Services
{
    public class CardPurchaseDomainService : ICardPurchaseDomainService
    {
        private readonly ICardInstallmentRepository _cardInstallmentRepository;
        private readonly ICardInvoiceRepository _cardInvoiceRepository;
        
        public CardPurchaseDomainService(ICardInstallmentRepository cardInstallmentRepository, ICardInvoiceRepository cardInvoiceRepository)
        {
            _cardInstallmentRepository = cardInstallmentRepository;
            _cardInvoiceRepository = cardInvoiceRepository;
        }

        public async Task GenerateInvoicesAndInstallmentsAsync(
            Card card,
            CardPurchase purchase,
            DateTime purchaseDate
        )
        {
            var installmentValue = purchase.TotalAmount / purchase.Installments;

            var baseDate = new DateTime(purchaseDate.Year, purchaseDate.Month, 1);

            if (purchaseDate.Day > card.ClosingDay)
                baseDate = baseDate.AddMonths(1);

            for (int i = 0; i < purchase.Installments; i++)
            {
                var closingDate = new DateTime(
                    baseDate.AddMonths(i).Year,
                    baseDate.AddMonths(i).Month,
                    card.ClosingDay
                );

                var dueDate = new DateTime(
                    baseDate.AddMonths(i).Year,
                    baseDate.AddMonths(i).Month,
                    card.DueDay
                );

                var invoice = await _cardInvoiceRepository.GetOrCreateAsync(card.Id, closingDate, dueDate);
                invoice.AddAmount(installmentValue);

                var newCardInstallment = new CardInstallment(
                    purchase.Id,
                    invoice.Id,
                    i + 1,
                    purchase.Installments,
                    installmentValue
                );

                await _cardInstallmentRepository.AddAsync(newCardInstallment);
            }
        }
    }
}