using Financeasy.Application.Services;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using MediatR;

namespace Financeasy.Application.UseCases.CardPurchaseCases.CreateCardPurchase
{
    public class CreateCardPurchaseHandler : IRequestHandler<CreateCardPurchaseCommand, Guid>
    {
        private readonly ICardPurchaseRepository _cardPurchaseRepository;
        private readonly ICardRepository _cardRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICardPurchaseDomainService _purchaseDomainService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCardPurchaseHandler(
            ICardPurchaseRepository cardPurchaseRepository, 
            ICardRepository cardRepository, 
            ICategoryRepository categoryRepository,
            ICardPurchaseDomainService purchaseDomainService, 
            IUnitOfWork unitOfWork)
        {
            _cardPurchaseRepository = cardPurchaseRepository;
            _cardRepository = cardRepository;
            _categoryRepository = categoryRepository;
            _purchaseDomainService = purchaseDomainService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateCardPurchaseCommand request, CancellationToken cancellationToken)
        {
            var cardExist = await _cardRepository.GetByIdAsync(request.CardId, cancellationToken);
            if (cardExist is null)
                throw new ArgumentException($"Não foi encontrado nenhum cartão com esse id {request.CardId}");

            if(request.TotalAmount > cardExist.AvailableLimit)
                throw new ArgumentOutOfRangeException("Não há limite suficiente disponível no cartão de crédito escolhido");

            var newCardPurchase = new CardPurchase(
                request.UserId,
                request.CardId,
                request.CategoryId,
                request.TotalAmount,
                request.Installments,
                request.PurchaseDate,
                request.Description
            );

            var cardCategory = await _categoryRepository.GetByIdAsync(newCardPurchase.CategoryId, cancellationToken);

            await _cardPurchaseRepository.AddAsync(newCardPurchase, cancellationToken);

            await _purchaseDomainService.GenerateInvoicesAndInstallmentsAsync(cardExist, newCardPurchase, cardCategory!.Name, newCardPurchase.PurchaseDate, cancellationToken);

            cardExist.DecreaseAvailableLimit(newCardPurchase.TotalAmount);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return newCardPurchase.Id;
        }
    }
}