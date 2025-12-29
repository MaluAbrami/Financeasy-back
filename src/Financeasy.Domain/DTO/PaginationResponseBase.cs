namespace Financeasy.Domain.DTO
{
    public record PaginationResponseBase
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
    }
}