using Financeasy.Domain.DTO;
using MediatR;

namespace Financeasy.Application.UseCases.CategoryCases.GetAllCategorys
{
    public record GetAllCategorysResponse()
    {
        public List<CategoryResponseDTO> Categorys { get; set; }
    }

    public record GetAllCategorys : IRequest<GetAllCategorysResponse>
    {
        public Guid UserId { get; set; }
    }
}