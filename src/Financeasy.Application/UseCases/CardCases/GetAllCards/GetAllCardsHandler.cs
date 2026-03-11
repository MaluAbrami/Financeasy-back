using System.Linq.Expressions;
using Financeasy.Domain.DTO.Pagination;
using Financeasy.Domain.Enums;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using MediatR;

namespace Financeasy.Application.UseCases.CardCases.GetAllCards
{
    public class GetAllCardsHandler : IRequestHandler<GetAllCardsQuery, GetAllCardsResponse>
    {
        private readonly ICardRepository _cardRepository;
        private readonly ICardInvoiceDapperRepository _invoiceDapperRepository;

        public GetAllCardsHandler(
            ICardRepository cardRepository,
            ICardInvoiceDapperRepository invoiceDapperRepository
        )
        {
            _cardRepository = cardRepository;
            _invoiceDapperRepository = invoiceDapperRepository;
        }

        public async Task<GetAllCardsResponse> Handle(GetAllCardsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Card, object>> expression =
                request.OrderBy switch
                {
                    CardOrderBy.ClosingDay => x => x.ClosingDay,
                    CardOrderBy.CreditLimit => x => x.CreditLimit,
                    CardOrderBy.DueDay => x => x.DueDay,
                    CardOrderBy.Name => x => x.Name,
                    _ => x => x.CreditLimit
                };

            var getCardsPaged = await _cardRepository.GetPagedWithRelationsAsync(
                x => x.UserId == request.UserId,
                expression,
                request.Direction == SortDirection.Asc
                ? true
                : false,
                request.Pagination.Page,
                request.Pagination.PageSize,
                cancellationToken
            );

            foreach (var card in getCardsPaged.List)
            {
                card.UsedLimit = await _invoiceDapperRepository.GetTotalAmountUnpaidByCardId(card.Id, cancellationToken);
            }

            return new GetAllCardsResponse
            {
                Cards = getCardsPaged.List,
                Pagination = new PaginationResponseBase
                {
                    Page = request.Pagination.Page,
                    PageSize = request.Pagination.PageSize,
                    TotalItems = getCardsPaged.TotalItems,
                    TotalPages = (int)Math.Ceiling(
                        getCardsPaged.TotalItems / (double)request.Pagination.PageSize
                    )
                }
            };
        }
    }
}