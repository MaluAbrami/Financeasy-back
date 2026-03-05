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
        private readonly ICardPurchaseDapperRepository _purchaseDapperRepository;

        public GetAllPurchasesByCardHandler(ICardPurchaseDapperRepository purchaseDapperRepository)
        {
            _purchaseDapperRepository = purchaseDapperRepository;
        }

        public async Task<GetAllPurchasesByCardResponse> Handle(GetAllPurchasesByCardQuery request, CancellationToken cancellationToken)
        {
            var cardsPurchases = await _purchaseDapperRepository.GetPagedWithRelationsByCardAsync(
                request.CardId,
                request.OrderBy.ToString(),
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