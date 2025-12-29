using Financeasy.Domain.DTO;
using MediatR;

namespace Financeasy.Application.UseCases.CategoryCases.GetAllCategorys
{
    public record GetAllCategorysRespone()
    {
        public List<CategoryResponseDTO> Categorys { get; set; }
    }

    public record GetAllCategorys : IRequest<GetAllCategorysRespone>
    {
        public Guid UserId { get; set; }
    }
}