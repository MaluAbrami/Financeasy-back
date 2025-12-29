using Financeasy.Application.UseCases.CategoryCases.GetAllCategorys;
using MediatR;

namespace Financeasy.Application.UseCases.CategoryCases.GetAllFixedCategorys
{
    public record GetAllFixedCategorys : IRequest<GetAllCategorysRespone>
    {
        public Guid UserId { get; set; }
    }
}