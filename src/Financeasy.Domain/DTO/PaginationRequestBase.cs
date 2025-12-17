namespace Financeasy.Domain.DTO
{
    public record PaginationRequestBase
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}