using Financeasy.Application.UseCases.CardCases.CreateCard;
using Financeasy.Application.UseCases.CardCases.DeleteCard;
using Financeasy.Application.UseCases.CardCases.GetAllCards;
using Financeasy.Application.UseCases.CardCases.UpdateCard;
using Financeasy.Domain.DTO.Card;
using Financeasy.Domain.DTO.Pagination;
using Financeasy.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Financeasy.Api.Endpoints
{
    public static class CardEndpoint
    {
        public static RouteGroupBuilder MapCards(this RouteGroupBuilder group)
        {
            group.MapPost("", CreateCard)
                .RequireAuthorization();

            group.MapGet("/get-all/{page}/{pageSize}/{orderBy}/{direction}", GetAllCards)
                .RequireAuthorization();

            group.MapDelete("/{cardId}", DeleteCard)
                .RequireAuthorization();

            group.MapPatch("/update/{cardId}", UpdateCard)
                .RequireAuthorization();

            return group;
        }

        private static async Task<IResult> CreateCard(CreateCardRequest request, HttpContext context, IMediator mediator)
        {
            var userId = context.User.FindFirst("userId")?.Value;

            if(userId is null)
                return Results.Unauthorized();

            await mediator.Send(new CreateCardCommand { 
                    UserId = Guid.Parse(userId), 
                    BankAccountId = request.BankAccountId,
                    Name = request.Name,
                    CreditLimit = request.CreditLimit,
                    ClosingDay = request.ClosingDay,
                    DueDay = request.DueDay
                }
            );

            return Results.Created();
        }

        private static async Task<IResult> GetAllCards(
            HttpContext context, 
            IMediator mediator,
            int page = 1,
            int pageSize = 10,
            CardOrderBy orderBy = CardOrderBy.CreditLimit,
            SortDirection direction = SortDirection.Asc)
        {
            var userId = context.User.FindFirst("userId")?.Value;

            if(userId is null)
                return Results.Unauthorized();

            var cards = await mediator.Send(new GetAllCardsQuery { 
                    UserId = Guid.Parse(userId), 
                    Pagination = new PaginationRequestBase
                    {
                        Page = page,
                        PageSize = pageSize
                    },
                    OrderBy = orderBy,
                    Direction = direction
                }
            );

            return Results.Ok(cards);
        }

        private static async Task<IResult> DeleteCard(Guid cardId, HttpContext context, IMediator mediator)
        {
            var userId = context.User.FindFirst("userId")?.Value;

            if(userId is null)
                return Results.Unauthorized();

            await mediator.Send(new DeleteCardCommand { 
                    UserId = Guid.Parse(userId), 
                    CardId = cardId
                }
            );

            return Results.NoContent();
        }

        private static async Task<IResult> UpdateCard(
            Guid cardId, 
            HttpContext context, 
            IMediator mediator,
            string? name = null,
            int closingDay = 0,
            int dueDay = 0,
            decimal creditLimit = 0)
        {
            var userId = context.User.FindFirst("userId")?.Value;

            if(userId is null)
                return Results.Unauthorized();

            var response = await mediator.Send(new UpdateCardCommand { 
                    UserId = Guid.Parse(userId), 
                    CardId = cardId,
                    Name = name,
                    ClosingDay = closingDay,
                    DueDay = dueDay,
                    CreditLimit = creditLimit
                }
            );

            return Results.Ok(response);
        }
    }
}