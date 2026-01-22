using MediatR;

namespace Financeasy.Application.UseCases.CardPurchaseCases.DeleteCardPurchase
{
    public class DeleteCardPurchaseCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public Guid CardPurchaseId { get; set; }
    }
}