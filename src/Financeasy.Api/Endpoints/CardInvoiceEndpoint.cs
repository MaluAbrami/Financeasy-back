using Financeasy.Application.UseCases.CardInvoiceCases.GetAllCardInvoicesByCard;
using Financeasy.Application.UseCases.CardInvoiceCases.GetCardInvoiceByPeriod;
using Financeasy.Application.UseCases.CardInvoiceCases.PayCardInvoice;
using Financeasy.Domain.DTO.CardInvoice;
using Financeasy.Domain.DTO.Pagination;
using Financeasy.Domain.Enums;
using MediatR;

namespace Financeasy.Api.Endpoints
{
    public static class CardInvoiceEndpoint
    {
        public static RouteGroupBuilder MapCardInvoice(this RouteGroupBuilder group)
        {
            group.MapPost("/pay-card-invoice", PayCardInvoice)
                .RequireAuthorization();

            group.MapGet("/get-by-period/{cardId}/{month}/{year}", GetInvoiceByPeriod)
                .RequireAuthorization();

            group.MapGet("/get-all-by-card/{cardId}/{page}/{pageSize}/{orderBy}/{direction}", GetAllInvoicesByCard)
                .RequireAuthorization();

            return group;
        }

        private static async Task<IResult> PayCardInvoice(PayCardInvoiceRequest request, HttpContext context, IMediator mediator)
        {
            var userId = context.User.FindFirst("userId")?.Value;
            if(userId is null)
                return Results.Unauthorized();

            await mediator.Send(new PayCardInvoiceCommand { UserId = Guid.Parse(userId), CardId = request.CardId, ClosingDate = request.ClosingDate });

            return Results.Ok();
        }

        private static async Task<IResult> GetInvoiceByPeriod(Guid cardId, int month, int year, HttpContext context, IMediator mediator)
        {
            var userId = context.User.FindFirst("userId")?.Value;
            if(userId is null)
                return Results.Unauthorized();

            var response = await mediator.Send(new GetInvoiceByPeriodQuery { CardId = cardId, Month = month, Year = year });

            return Results.Ok(response);
        }

        private static async Task<IResult> GetAllInvoicesByCard(
            Guid cardId,
            HttpContext context, 
            IMediator mediator,
            int page = 1,
            int pageSize = 10,
            CardInvoiceOrderBy orderBy = CardInvoiceOrderBy.ClosingDate,
            SortDirection direction = SortDirection.Asc)
        {
            var userId = context.User.FindFirst("userId")?.Value;
            if(userId is null)
                return Results.Unauthorized();

            var response = await mediator.Send(new GetAllInvoicesByCardQuery 
                { 
                    CardId = cardId,
                    Pagination = new PaginationRequestBase
                    {
                        Page = page,
                        PageSize = pageSize
                    },
                    OrderBy = orderBy,
                    Direction = direction
                }
            );

            return Results.Ok(response);
        }
    }
}