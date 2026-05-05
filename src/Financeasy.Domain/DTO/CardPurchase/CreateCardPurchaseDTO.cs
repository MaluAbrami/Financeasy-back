namespace Financeasy.Domain.DTO.CardPurchase
{
    public record CreateCardPurchaseDTO
    {
        public Guid CardId { get; set; }
        public Guid CategoryId { get; set; }
        public decimal TotalAmount { get; set; }
        public int Installments { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string? Description { get; set; } = string.Empty;
    }
}