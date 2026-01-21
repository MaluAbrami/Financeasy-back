namespace Financeasy.Domain.DTO.Card
{
    public class GetPagedCardDTO
    {
        public List<GetCardResponseDTO> List { get; set; }
        public int TotalItems { get; set; }
    }
}