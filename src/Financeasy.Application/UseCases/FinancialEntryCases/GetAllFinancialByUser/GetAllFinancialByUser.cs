using Financeasy.Domain.DTO;
using Financeasy.Domain.Enums;
using MediatR;

namespace Financeasy.Application.UseCases.FinancialEntryCases.GetAllFinancialByUser
{
    public record GetAllFinancialResponse()
    {
        public List<FinancialResponseDTO>? FinancialsByUser { get; set; }
        public PaginationResponseBase Pagination { get; set; }
    }

    public record GetAllFinancialByUser : IRequest<GetAllFinancialResponse>
    {
        public Guid UserId { get; set; }
        public PaginationRequestBase Pagination { get; set; }
        public FinancialEntryOrderBy OrderBy { get; set; } = FinancialEntryOrderBy.Date;
        public SortDirection Direction { get; set; } = SortDirection.Asc;
    }
}