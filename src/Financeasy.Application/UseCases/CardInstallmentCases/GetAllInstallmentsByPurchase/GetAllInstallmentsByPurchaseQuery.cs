using Financeasy.Domain.DTO.CardInstallment;
using Financeasy.Domain.DTO.Pagination;
using Financeasy.Domain.Enums;
using MediatR;

namespace Financeasy.Application.UseCases.CardInstallmentCases.GetAllInstallmentsByPurchase
{
    public record GetAllInstallmentsByPurchaseResponse()
    {
        public List<GetInstallmentResponseDTO> Installments { get; set; }
        public PaginationResponseBase Pagination { get; set; }
    }

    public record GetAllInstallmentsByPurchaseQuery : IRequest<GetAllInstallmentsByPurchaseResponse>
    {
        public Guid PurchaseId { get; set; }
        public PaginationRequestBase Pagination { get; set; }
        public CardInstallmentOrderBy OrderBy { get; set; }
        public SortDirection Direction { get; set; }
    }
}