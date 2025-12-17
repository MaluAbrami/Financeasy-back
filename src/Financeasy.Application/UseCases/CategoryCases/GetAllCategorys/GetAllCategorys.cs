using Financeasy.Domain.DTO;
using Financeasy.Domain.Enums;
using MediatR;

namespace Financeasy.Application.UseCases.CategoryCases.GetAllCategorys
{
    public record GetAllCategorysResponse()
    {
        public List<CategoryResponseDTO> Categorys { get; set; }
        public PaginationResponseBase Pagination { get; set; }
    }

    public record GetAllCategorys : IRequest<GetAllCategorysResponse>
    {
        public Guid UserId { get; set; }
        public PaginationRequestBase Pagination { get; set; }
        public CategoryOrderBy OrderBy { get; set; } = CategoryOrderBy.Name;
        public SortDirection Direction { get; set; } = SortDirection.Asc;
    }
}