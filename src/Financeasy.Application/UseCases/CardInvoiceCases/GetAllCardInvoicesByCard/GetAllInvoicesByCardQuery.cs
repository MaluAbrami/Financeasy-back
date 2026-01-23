using Financeasy.Domain.DTO.CardInvoice;
using Financeasy.Domain.DTO.Pagination;
using Financeasy.Domain.Enums;
using MediatR;

namespace Financeasy.Application.UseCases.CardInvoiceCases.GetAllCardInvoicesByCard
{
    public record GetAllInvoicesByCardResponse()
    {
        public List<GetInvoiceResponseDTO> Invoices { get; set; }
        public PaginationResponseBase Pagination { get; set; }
    }

    public record GetAllInvoicesByCardQuery : IRequest<GetAllInvoicesByCardResponse>
    {
        public Guid CardId { get; set; }
        public PaginationRequestBase Pagination { get; set; }
        public CardInvoiceOrderBy OrderBy { get; set; }
        public SortDirection Direction { get; set; }
    }
}