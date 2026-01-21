using Financeasy.Domain.DTO.Card;
using Financeasy.Domain.DTO.Pagination;
using Financeasy.Domain.Enums;
using MediatR;

namespace Financeasy.Application.UseCases.CardCases.GetAllCards
{
    public record GetAllCardsResponse()
    {
        public List<GetCardResponseDTO> Cards { get; set; }
        public PaginationResponseBase Pagination { get; set; }
    }

    public record GetAllCardsQuery : IRequest<GetAllCardsResponse>
    {
        public Guid UserId { get; set; }
        public PaginationRequestBase Pagination { get; set; }
        public CardOrderBy OrderBy { get; set; }
        public SortDirection Direction { get; set; }
    }
}