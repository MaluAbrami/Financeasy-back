using Financeasy.Application.UseCases.CardCases.CreateCard;
using Financeasy.Domain.DTO.Card;
using MediatR;

namespace Financeasy.Api.Endpoints
{
    public static class CardEndpoint
    {
        public static RouteGroupBuilder MapCards(this RouteGroupBuilder group)
        {
            group.MapPost("", CreateCard)
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
    }
}