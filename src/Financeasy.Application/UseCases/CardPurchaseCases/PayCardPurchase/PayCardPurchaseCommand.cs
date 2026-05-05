using MediatR;

namespace Financeasy.Application.UseCases.CardPurchaseCases.PayCardPurchase
{
    public record PayCardPurchaseResponse
    (
        Guid PaymentTransactionId
    );

    public class PayCardPurchaseCommand : IRequest<PayCardPurchaseResponse>
    {
        public Guid UserId { get; set; }
        public Guid PurchaseId { get; set; }
        public int InstallmentsQuantity { get; set; }
        public decimal Amount { get; set; }
    }
}