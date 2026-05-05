namespace Financeasy.Domain.DTO.Alert
{
    public class GetPagedAlertDTO
    {
        public List<AlertResponseDTO> List { get; set; }
        public int TotalItems { get; set; }
    }
}