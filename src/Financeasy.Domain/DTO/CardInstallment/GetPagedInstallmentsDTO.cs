namespace Financeasy.Domain.DTO.CardInstallment
{
    public class GetPagedInstallmentsDTO
    {
        public List<GetInstallmentResponseDTO> List { get; set; }
        public int TotalItems { get; set; }
    }
}