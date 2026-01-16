using Financeasy.Application.UseCases.CardPurchaseCases.CreateCardPurchase;
using MediatR;

namespace Financeasy.Api.Endpoints
{
    public static class CardPurchaseEndpoint
    {
        public static RouteGroupBuilder MapCardPurchases(this RouteGroupBuilder group)
        {
            group.MapPost("", CreateCardPurchase)
                .RequireAuthorization();

            return group;
        }

        private static async Task<IResult> CreateCardPurchase(CreateCardPurchaseCommand command, HttpContext context, IMediator mediator)
        {
            var userId = context.User.FindFirst("userId")?.Value;
            if(userId is null)
                return Results.Unauthorized();

            await mediator.Send(command);

            return Results.Created();
        }
    }
}