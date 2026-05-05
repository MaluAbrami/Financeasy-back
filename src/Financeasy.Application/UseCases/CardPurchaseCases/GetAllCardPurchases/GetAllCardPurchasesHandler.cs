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
        private readonly ICardPurchaseDapperRepository _purchaseDapperRepository;

        public GetAllCardPurchasesHandler(ICardPurchaseDapperRepository purchaseDapperRepository)
        {
            _purchaseDapperRepository = purchaseDapperRepository;
        }

        public async Task<GetAllCardPurchasesResponse> Handle(GetAllCardPurchasesQuery request, CancellationToken cancellationToken)
        {
            var getPagedPurchases = await _purchaseDapperRepository.GetPagedWithRelationsAsync(
                request.UserId,
                request.OrderBy.ToString(),
                request.Direction == SortDirection.Asc
                ? true
                : false,
                request.Pagination.Page,
                request.Pagination.PageSize,
                cancellationToken
            );

            return new GetAllCardPurchasesResponse
            {
                CardPurchases = getPagedPurchases.List,
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