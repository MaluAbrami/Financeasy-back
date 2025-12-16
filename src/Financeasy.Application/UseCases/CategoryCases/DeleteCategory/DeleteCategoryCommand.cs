using MediatR;

namespace Financeasy.Application.UseCases.CategoryCases.DeleteCategory
{
    public record DeleteCategoryCommand : IRequest<DeleteCategoryCommand>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}