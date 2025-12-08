using Financeasy.Application.UseCases.FinancialEntryCases.CreateFinancialEntry;
using MediatR;

namespace Financeasy.Api.Endpoints
{
    public static class FinancialEntryEndpoint
    {
        public static RouteGroupBuilder MapFinancialEntryEndpoints(this RouteGroupBuilder group)
        {
            group.MapPost("", CreateFinancialEntry)
                .RequireAuthorization();

            return group;
        }

        private async static Task<IResult> CreateFinancialEntry(CreateFinancialEntryCommand command, HttpContext httpContext, IMediator mediator)
        {
            var userId = httpContext.User.FindFirst("userId")?.Value;

            if(userId is null)
                return Results.Unauthorized();

            command.UserId = Guid.Parse(userId);

            var id = await mediator.Send(command);

            return Results.Created();
        }
    }
}