using Financeasy.Domain.interfaces;
using Financeasy.Domain.interfaces.Services;
using Financeasy.Domain.models;

namespace Financeasy.Domain.Services
{
    public class CardPurchaseService : ICardPurchaseService
    {
        private readonly ICardPurchaseRepository _cardPurchaseRepository;
        private readonly ICardDapperRepository _cardDapperRepository;
        private readonly IInstallmentGeneratorService _installmentGeneratorService;
        private readonly ICardInvoiceDapperRepository _invoiceDapperRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CardPurchaseService(
            ICardPurchaseRepository cardPurchaseRepository,
            ICardDapperRepository cardDapperRepository,
            IInstallmentGeneratorService installmentGeneratorService,
            ICardInvoiceDapperRepository invoiceDapperRepository,
            IUnitOfWork unitOfWork
        )
        {
            _cardPurchaseRepository = cardPurchaseRepository;
            _cardDapperRepository = cardDapperRepository;
            _installmentGeneratorService = installmentGeneratorService;
            _invoiceDapperRepository = invoiceDapperRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> CreatePurchase(
            Guid userId, Guid cardId, Guid categoryId, decimal totalAmount, int installments, DateTime purchaseDate, string description, CancellationToken cancellationToken
        )
        {
            var card = await _cardDapperRepository.GetCardById(cardId, cancellationToken);

            if (card is null)
                throw new ArgumentException($"Não foi encontrado um cartão com o id {cardId}");
            
            var cardUsedLimit = await _invoiceDapperRepository.GetTotalAmountUnpaidByCardId(card.Id, cancellationToken);
            if((card.CreditLimit - cardUsedLimit) < totalAmount)
                throw new Exception("O cartão não possui limite suficiente disponível para realizar essa compra");

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

            await _installmentGeneratorService.GenerateInstallments(newCardPurchase, card, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return newCardPurchase.Id;
        }
    }
}