using Financeasy.Domain.DTO.CardPurchase;
using Financeasy.Domain.DTO.Pagination;
using Financeasy.Domain.Enums;
using MediatR;

namespace Financeasy.Application.UseCases.CardPurchaseCases.GetAllCardPurchases
{
    public record GetAllCardPurchasesResponse()
    {
        public List<GetCardPurchaseResponseDTO> Purchases { get; set; }
        public PaginationResponseBase Pagination { get; set; }
    }

    public record GetAllCardPurchasesQuery : IRequest<GetAllCardPurchasesResponse>
    {
        public Guid UserId { get; set; }
        public PaginationRequestBase Pagination { get; set; }
        public CardPurchaseOrderBy OrderBy { get; set; }
        public SortDirection Direction { get; set; }
    }
}