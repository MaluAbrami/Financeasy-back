using Financeasy.Domain.interfaces.Services;
using MediatR;

namespace Financeasy.Application.UseCases.CardPurchaseCases.CreateCardPurchase
{
    public class CreateCardPurchaseHandler : IRequestHandler<CreateCardPurchaseCommand, Guid>
    {
        private readonly ICardPurchaseService _cardPurchaseService;

        public CreateCardPurchaseHandler(
            ICardPurchaseService cardPurchaseService
        )
        {
            _cardPurchaseService = cardPurchaseService;
        }

        public async Task<Guid> Handle(CreateCardPurchaseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // if(request.TotalAmount > cardExist.AvailableLimit)
                //     throw new ArgumentOutOfRangeException("Não há limite suficiente disponível no cartão de crédito escolhido");

                return await _cardPurchaseService.CreatePurchase(
                    request.UserId,
                    request.CardId,
                    request.CategoryId,
                    request.TotalAmount,
                    request.Installments,
                    request.PurchaseDate,
                    request.Description,
                    cancellationToken
                );
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro: ", ex);
            }
        }
    }
}