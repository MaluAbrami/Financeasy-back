using Financeasy.Domain.DTO.Category;
using Financeasy.Domain.DTO.Pagination;
using Financeasy.Domain.Enums;
using MediatR;

namespace Financeasy.Application.UseCases.CategoryCases.GetAllCategorysPaged
{
    public record GetAllCategorysPagedResponse()
    {
        public List<CategoryResponseDTO> Categorys { get; set; }
        public PaginationResponseBase Pagination { get; set; }
    }

    public record GetAllCategorysPaged : IRequest<GetAllCategorysPagedResponse>
    {
        public Guid UserId { get; set; }
        public PaginationRequestBase Pagination { get; set; }
        public CategoryOrderBy OrderBy { get; set; } = CategoryOrderBy.Name;
        public SortDirection Direction { get; set; } = SortDirection.Asc;
    }
}