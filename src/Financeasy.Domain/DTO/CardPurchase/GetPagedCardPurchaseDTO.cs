namespace Financeasy.Domain.DTO.CardPurchase
{
    public class GetPagedCardPurchaseDTO
    {
        public List<GetCardPurchaseResponseDTO> List { get; set; }
        public int TotalItems { get; set; }
    }
}