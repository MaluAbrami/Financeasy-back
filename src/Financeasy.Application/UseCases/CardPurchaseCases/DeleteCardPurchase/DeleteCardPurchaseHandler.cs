using Financeasy.Application.Services;
using Financeasy.Domain.interfaces;
using MediatR;

namespace Financeasy.Application.UseCases.CardPurchaseCases.DeleteCardPurchase
{
    public class DeleteCardPurchaseHandler : IRequestHandler<DeleteCardPurchaseCommand, Unit>
    {
        private readonly ICardPurchaseRepository _cardPurchaseRepository;
        private readonly ICardRepository _cardRepository;
        private readonly ICardPurchaseDomainService _cardPurchaseService;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCardPurchaseHandler(
            ICardPurchaseRepository cardPurchaseRepository,
            ICardRepository cardRepository,
            ICardPurchaseDomainService cardPurchaseService,
            IUnitOfWork unitOfWork)
        {
            _cardPurchaseRepository = cardPurchaseRepository;
            _cardRepository = cardRepository;
            _cardPurchaseService = cardPurchaseService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteCardPurchaseCommand request, CancellationToken cancellationToken)
        {
            var cardPurchase = await _cardPurchaseRepository.GetByIdAsync(request.CardPurchaseId, cancellationToken);

            if(cardPurchase is null)
                throw new ArgumentException("Cartão não encontrado");

            if(cardPurchase.UserId != request.UserId)
                throw new UnauthorizedAccessException("Usuário não tem acesso a esta ação");

            await _cardPurchaseService.DeleteInstallmentsAndDecreaseInvoiceAsync(request.CardPurchaseId, cancellationToken);

            var card = await _cardRepository.GetByIdAsync(cardPurchase.CardId, cancellationToken);
            card!.IncreaseAvailableLimit(cardPurchase.TotalAmount);

            _cardPurchaseRepository.Delete(cardPurchase);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}