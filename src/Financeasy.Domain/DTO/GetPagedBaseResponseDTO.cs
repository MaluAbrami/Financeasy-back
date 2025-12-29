namespace Financeasy.Domain.DTO
{
    public class GetPagedBaseResponseDTO<T>
    {
        public List<T> List {get; set; } = [];
        public int TotalItems { get; set; }
    }
}