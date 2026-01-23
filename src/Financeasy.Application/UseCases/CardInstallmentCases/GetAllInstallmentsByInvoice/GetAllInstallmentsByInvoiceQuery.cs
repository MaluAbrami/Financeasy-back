using Financeasy.Domain.DTO.CardInstallment;
using Financeasy.Domain.DTO.Pagination;
using Financeasy.Domain.Enums;
using MediatR;

namespace Financeasy.Application.UseCases.CardInstallmentCases.GetAllInstallmentsByInvoice
{
    public record GetAllInstallmentsByInvoiceResponse()
    {
        public List<GetInstallmentResponseDTO> Installments { get; set; }
        public PaginationResponseBase Pagination { get; set; }
    }

    public record GetAllInstallmentsByInvoiceQuery : IRequest<GetAllInstallmentsByInvoiceResponse>
    {
        public Guid InvoiceId { get; set; }
        public PaginationRequestBase Pagination { get; set; }
        public CardInstallmentOrderBy OrderBy { get; set; }
        public SortDirection Direction { get; set; }
    }
}