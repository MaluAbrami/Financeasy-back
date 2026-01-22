using System.Linq.Expressions;
using Financeasy.Domain.DTO.Pagination;
using Financeasy.Domain.Enums;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using MediatR;

namespace Financeasy.Application.UseCases.CardPurchaseCases.GetAllCardPurchasesByCard
{
    public class GetAllPurchasesByCardHandler : IRequestHandler<GetAllPurchasesByCardQuery, GetAllPurchasesByCardResponse>
    {
        private readonly ICardPurchaseRepository _cardPurchaseRepository;

        public GetAllPurchasesByCardHandler(ICardPurchaseRepository cardPurchaseRepository)
        {
            _cardPurchaseRepository = cardPurchaseRepository;
        }

        public async Task<GetAllPurchasesByCardResponse> Handle(GetAllPurchasesByCardQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<CardPurchase, object>> expression =
                request.OrderBy switch
                {
                    CardPurchaseOrderBy.PurchaseDate => x => x.PurchaseDate,
                    CardPurchaseOrderBy.TotalAmount => x => x.TotalAmount,
                    _ => x => x.PurchaseDate
                };

            var cardsPurchases = await _cardPurchaseRepository.GetPagedWithRelationsAsync(
                x => x.CardId == request.CardId,
                expression,
                request.Direction == SortDirection.Asc
                ? true
                : false,
                request.Pagination.Page,
                request.Pagination.PageSize,
                cancellationToken
            );

            return new GetAllPurchasesByCardResponse
            {
                CardPurchases = cardsPurchases.List,
                Pagination = new PaginationResponseBase
                {
                    Page = request.Pagination.Page,
                    PageSize = request.Pagination.PageSize,
                    TotalItems = cardsPurchases.TotalItems,
                    TotalPages = (int)Math.Ceiling(
                        cardsPurchases.TotalItems / (double)request.Pagination.PageSize
                    )
                }
            };
        }
    }
}