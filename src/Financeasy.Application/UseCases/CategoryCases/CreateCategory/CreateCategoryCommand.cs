using Financeasy.Domain.Enums;
using MediatR;

namespace Financeasy.Application.UseCases.CategoryCases.CreateCategory
{
    public record CreateCategoryCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public required string Name { get; set; }
        public EntryType Type { get; set; }
        public bool IsFixed { get; set; }
        public int? Recurrence { get; set; }
    }
}