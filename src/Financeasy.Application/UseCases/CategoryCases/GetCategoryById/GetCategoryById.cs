using Financeasy.Domain.Enums;
using MediatR;

namespace Financeasy.Application.UseCases.CategoryCases.GetCategoryById
{
    public record GetCategoryByIdResponse()
    {
        public string Name { get; set; }
        public EntryType Type { get; set; }
        public RecurrenceType RecurrenceType { get; set; }
    }

    public record GetCategoryById : IRequest<GetCategoryByIdResponse>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}