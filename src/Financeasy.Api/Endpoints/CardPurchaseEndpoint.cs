using Financeasy.Application.UseCases.CardPurchaseCases.CreateCardPurchase;
using Financeasy.Application.UseCases.CardPurchaseCases.DeleteCardPurchase;
using Financeasy.Application.UseCases.CardPurchaseCases.GetAllCardPurchases;
using Financeasy.Domain.DTO.CardPurchase;
using Financeasy.Domain.DTO.Pagination;
using Financeasy.Domain.Enums;
using MediatR;

namespace Financeasy.Api.Endpoints
{
    public static class CardPurchaseEndpoint
    {
        public static RouteGroupBuilder MapCardPurchases(this RouteGroupBuilder group)
        {
            group.MapPost("", CreateCardPurchase)
                .RequireAuthorization();

            
            group.MapGet("/get-all/{page}/{pageSize}/{orderBy}/{direction}", GetAllCardsPurchases)
                .RequireAuthorization();

            group.MapDelete("/{id}", DeleteCardPurchase)
                .RequireAuthorization();

            return group;
        }

        private static async Task<IResult> CreateCardPurchase(CreateCardPurchaseDTO request, HttpContext context, IMediator mediator)
        {
            var userId = context.User.FindFirst("userId")?.Value;
            if(userId is null)
                return Results.Unauthorized();

            await mediator.Send(new CreateCardPurchaseCommand
            {
                UserId = Guid.Parse(userId),
                CardId = request.CardId,
                CategoryId = request.CategoryId,
                TotalAmount = request.TotalAmount,
                Installments = request.Installments,
                PurchaseDate = request.PurchaseDate,
                Description = request.Description
            });

            return Results.Created();
        }

        private static async Task<IResult> GetAllCardsPurchases(
            HttpContext context, 
            IMediator mediator,
            int page = 1,
            int pageSize = 10,
            CardPurchaseOrderBy orderBy = CardPurchaseOrderBy.PurchaseDate,
            SortDirection direction = SortDirection.Asc)
        {
            var userId = context.User.FindFirst("userId")?.Value;
            if(userId is null)
                return Results.Unauthorized();

            var cardsPurchases = await mediator.Send(new GetAllCardPurchasesQuery
            {
                UserId = Guid.Parse(userId),
                Pagination = new PaginationRequestBase
                {
                    Page = page,
                    PageSize = pageSize
                }
            });

            return Results.Ok(cardsPurchases);
        }

        private static async Task<IResult> DeleteCardPurchase(
            Guid id,
            HttpContext context, 
            IMediator mediator)
        {
            var userId = context.User.FindFirst("userId")?.Value;
            if(userId is null)
                return Results.Unauthorized();

            await mediator.Send(new DeleteCardPurchaseCommand
            {
                UserId = Guid.Parse(userId),
                CardPurchaseId = id
            });

            return Results.NoContent();
        }
    }
}