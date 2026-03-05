using Financeasy.Domain.DTO.CardPurchase;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;

namespace Financeasy.Domain.Services
{
    public class CardPurchaseService
    {
        private readonly ICardPurchaseRepository _cardPurchaseRepository;
        private readonly ICardInstallmentRepository _cardInstallmentRepository;
        private readonly ICardInvoiceRepository _cardInvoiceRepository;
        private readonly ICardInvoiceDapperRepository _cardInvoiceDapperRepository;
        private readonly ICardDapperRepository _cardDapperRepository;

        public CardPurchaseService(
            ICardPurchaseRepository cardPurchaseRepository,
            ICardInstallmentRepository cardInstallmentRepository,
            ICardInvoiceRepository cardInvoiceRepository,
            ICardInvoiceDapperRepository cardInvoiceDapperRepository,
            ICardDapperRepository cardDapperRepository
        )
        {
            _cardPurchaseRepository = cardPurchaseRepository;
            _cardInstallmentRepository = cardInstallmentRepository;
            _cardInvoiceRepository = cardInvoiceRepository;
            _cardInvoiceDapperRepository = cardInvoiceDapperRepository;
            _cardDapperRepository = cardDapperRepository;
        }

        public async Task<Guid> CreatePurchase(
            Guid userId, Guid cardId, Guid categoryId, decimal totalAmount, int installments, DateTime purchaseDate, string description, CancellationToken cancellationToken
        )
        {
            try
            {
                var newCardPurchase = new CardPurchase(
                    userId,
                    cardId,
                    categoryId,
                    totalAmount,
                    installments,
                    purchaseDate,
                    description
                );

                await _cardPurchaseRepository.AddAsync(newCardPurchase, cancellationToken);

                var card = await _cardDapperRepository.GetCardById(newCardPurchase.CardId, cancellationToken);

                if (card is null)
                    throw new ArgumentException($"Não foi encontrado um cartão com o id {newCardPurchase.CardId}");

                var (closingDate, dueDate) = GetClosingAndDueDate(card, newCardPurchase);

                var invoice = await _cardInvoiceDapperRepository.GetCardInvoiceByClosingDate(closingDate, cancellationToken);
                
                if(invoice is null)
                    invoice = await GenerateInvoice(card.Id, closingDate, dueDate, cancellationToken);

                await GenerateInstallments(newCardPurchase, invoice.Id, cancellationToken);

                return newCardPurchase.Id;
            }
            catch
            {
                throw;
            }
        }

        private (DateTime closingDate, DateTime dueDate) GetClosingAndDueDate(Card card, CardPurchase purchase)
        {
            var closingDate = new DateTime(purchase.PurchaseDate.Year, purchase.PurchaseDate.Month, card.DueDay);
            var dueDate = new DateTime(purchase.PurchaseDate.Year, purchase.PurchaseDate.Month, card.DueDay);
            if (purchase.PurchaseDate.Day >= card.ClosingDay)
            {
                closingDate.AddMonths(1);
                dueDate.AddMonths(1);
            }

            return (closingDate, dueDate);
        }

        private async Task<CardInvoice> GenerateInvoice(
            Guid cardId, DateTime closingDate, DateTime dueDate, CancellationToken cancellationToken
        )
        {
            try
            {
                var newInvoice = new CardInvoice
                (
                    cardId,
                    closingDate,
                    dueDate
                );

                await _cardInvoiceRepository.AddAsync(newInvoice, cancellationToken);

                return newInvoice;
            }
            catch
            {
                throw;
            }
        }

        private async Task GenerateInstallments(
            CardPurchase purchase, Guid invoiceId, CancellationToken cancellationToken
        )
        {
            try
            {
                var installmentAmount = purchase.TotalAmount / purchase.Installments;

                for(int i = 1; i <= purchase.Installments; i++)
                {
                    var newInstallment = new CardInstallment
                    (
                        purchase.Id,
                        invoiceId,
                        i,
                        installmentAmount
                    );

                    await _cardInstallmentRepository.AddAsync(newInstallment, cancellationToken);
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task PayInstallments()
        {
            
        }
    }
}