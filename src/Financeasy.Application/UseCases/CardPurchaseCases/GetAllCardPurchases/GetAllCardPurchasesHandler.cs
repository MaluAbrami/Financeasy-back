using System.Linq.Expressions;
using Financeasy.Domain.DTO.Pagination;
using Financeasy.Domain.Enums;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using MediatR;

namespace Financeasy.Application.UseCases.CardPurchaseCases.GetAllCardPurchases
{
    public class GetAllCardPurchasesHandler : IRequestHandler<GetAllCardPurchasesQuery, GetAllCardPurchasesResponse>
    {
        private readonly ICardPurchaseRepository _cardPurchaseRepository;

        public GetAllCardPurchasesHandler(ICardPurchaseRepository cardPurchaseRepository)
        {
            _cardPurchaseRepository = cardPurchaseRepository;
        }

        public async Task<GetAllCardPurchasesResponse> Handle(GetAllCardPurchasesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<CardPurchase, object>> expression =
                request.OrderBy switch
            {
                CardPurchaseOrderBy.TotalAmount => x => x.TotalAmount,
                CardPurchaseOrderBy.PurchaseDate => x => x.PurchaseDate,
                _ => x => x.PurchaseDate
            };

            var getPagedPurchases = await _cardPurchaseRepository.GetPagedWithRelationsAsync(
                x => x.UserId == request.UserId,
                expression,
                request.Direction == SortDirection.Asc
                ? true
                : false,
                request.Pagination.Page,
                request.Pagination.PageSize,
                cancellationToken
            );

            return new GetAllCardPurchasesResponse
            {
                Purchases = getPagedPurchases.List,
                Pagination = new PaginationResponseBase
                {
                    Page = request.Pagination.Page,
                    PageSize = request.Pagination.PageSize,
                    TotalItems = getPagedPurchases.TotalItems,
                    TotalPages = (int)Math.Ceiling(
                        getPagedPurchases.TotalItems / (double)request.Pagination.PageSize
                    )
                }
            };
        }
    }
}