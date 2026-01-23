using Financeasy.Domain.interfaces;
using MediatR;

namespace Financeasy.Application.UseCases.CardCases.DeleteCard
{
    public class DeleteCardHandler : IRequestHandler<DeleteCardCommand, Unit>
    {
        private readonly ICardRepository _cardRepository;
        private readonly ICardPurchaseRepository _cardPurchaseRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        
        public DeleteCardHandler(
            ICardRepository cardRepository, 
            ICardPurchaseRepository cardPurchaseRepository,
            ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork)
        {
            _cardRepository = cardRepository;
            _cardPurchaseRepository = cardPurchaseRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteCardCommand request, CancellationToken cancellationToken)
        {
            var card = await _cardRepository.GetByIdAsync(request.CardId, cancellationToken);

            if(card is null)
                throw new ArgumentException("Cartão não encontrado.");

            if(card.UserId != request.UserId)
                throw new UnauthorizedAccessException("Usuário não tem acesso a esta ação.");

            var purchases = await _cardPurchaseRepository.FindAsync(x => x.CardId == request.CardId, cancellationToken);
            if(purchases.Any())
                card.DisableCard();  
            else
            {
                var category = await _categoryRepository.GetByIdAsync(card.CategoryId, cancellationToken);
                _categoryRepository.Delete(category);

                _cardRepository.Delete(card);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}