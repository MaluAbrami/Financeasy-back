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
        private readonly IUnitOfWork _unitOfWork;

        public CardPurchaseService(
            ICardPurchaseRepository cardPurchaseRepository,
            ICardDapperRepository cardDapperRepository,
            IInstallmentGeneratorService installmentGeneratorService,
            IUnitOfWork unitOfWork
        )
        {
            _cardPurchaseRepository = cardPurchaseRepository;
            _cardDapperRepository = cardDapperRepository;
            _installmentGeneratorService = installmentGeneratorService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> CreatePurchase(
            Guid userId, Guid cardId, Guid categoryId, decimal totalAmount, int installments, DateTime purchaseDate, string description, CancellationToken cancellationToken
        )
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

            await _installmentGeneratorService.GenerateInstallments(newCardPurchase, card, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return newCardPurchase.Id;
        }
    }
}