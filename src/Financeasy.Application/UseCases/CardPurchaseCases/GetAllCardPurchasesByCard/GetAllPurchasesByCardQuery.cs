using Financeasy.Domain.DTO.CardPurchase;
using Financeasy.Domain.DTO.Pagination;
using Financeasy.Domain.Enums;
using MediatR;

namespace Financeasy.Application.UseCases.CardPurchaseCases.GetAllCardPurchasesByCard
{
    public record GetAllPurchasesByCardResponse()
    {
        public List<GetCardPurchaseResponseDTO> CardPurchases { get; set; }
        public PaginationResponseBase Pagination { get; set; }
    }

    public record GetAllPurchasesByCardQuery : IRequest<GetAllPurchasesByCardResponse>
    {
        public Guid CardId { get; set; }
        public PaginationRequestBase Pagination { get; set; }
        public CardPurchaseOrderBy OrderBy { get; set; }
        public SortDirection Direction { get; set; }
    }
}