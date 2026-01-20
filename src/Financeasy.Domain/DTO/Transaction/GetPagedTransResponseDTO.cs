namespace Financeasy.Domain.DTO.Transaction
{
    public record GetPagedTransResponseDTO
    {
        public List<GetTransactionResponseDTO> List { get; set; }
        public int TotalItems { get; set; }
    }
}