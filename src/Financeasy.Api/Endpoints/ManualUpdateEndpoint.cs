using Financeasy.Application.UseCases.CategoryCases.CreateCategory;
using Financeasy.Application.UseCases.ManualUpdatesCases.CreateManualUpdate;
using Financeasy.Application.UseCases.ManualUpdatesCases.GetLastUpdate;
using MediatR;

namespace Financeasy.Api.Endpoints
{
    public static class ManualUpdateEndpoint
    {
        public static RouteGroupBuilder MapUpdates(this RouteGroupBuilder group)
        {
            group.MapPost("", CreateManualUpdate)
                .RequireAuthorization();

            group.MapGet("/last", GetLastUpdate)
                .RequireAuthorization();

            return group;
        }

        private static async Task<IResult> CreateManualUpdate(HttpContext context, IMediator mediator)
        {
            var userId = context.User.FindFirst("userId")?.Value;
            if(userId is null)
                return Results.Unauthorized();

            await mediator.Send(new CreateManualUpdateCommand { UserId = Guid.Parse(userId) } );
            
            return Results.Created();
        }

        private static async Task<IResult> GetLastUpdate(HttpContext context, IMediator mediator)
        {
            var userId = context.User.FindFirst("userId")?.Value;
            if(userId is null)
                return Results.Unauthorized();

            var response = await mediator.Send(new GetLastUpdate { UserId = Guid.Parse(userId) } );
            
            return Results.Ok(response);
        }
    }
}