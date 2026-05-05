namespace Financeasy.Domain.DTO.Pagination
{
    public class GetPagedBaseResponseDTO<T>
    {
        public List<T> List {get; set; } = [];
        public int TotalItems { get; set; }
    }
}