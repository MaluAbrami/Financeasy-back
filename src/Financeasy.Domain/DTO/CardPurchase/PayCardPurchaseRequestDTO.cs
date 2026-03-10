namespace Financeasy.Domain.DTO.CardPurchase
{
    public class PayCardPurchaseRequestDTO
    {
        public Guid PurchaseId { get; set; }
        public int InstallmentsQuantity { get; set; }
        public decimal Amount { get; set; }
    }
}