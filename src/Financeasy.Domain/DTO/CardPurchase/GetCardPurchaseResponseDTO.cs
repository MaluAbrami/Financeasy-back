namespace Financeasy.Domain.DTO.CardPurchase
{
    public record GetCardPurchaseResponseDTO
    {
        public string CardName { get; set; }
        public string CategoryName { get; set; }
        public decimal TotalAmount { get; set; }
        public int Installments { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string? Description { get; set; }
    }
}