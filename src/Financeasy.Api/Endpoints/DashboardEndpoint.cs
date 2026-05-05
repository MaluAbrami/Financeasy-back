using Financeasy.Application.UseCases.DashboardsCases.SpendingMonthlyControl;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Financeasy.Api.Endpoints
{
    public static class DashboardEndpoint
    {
        public static RouteGroupBuilder MapDashboardEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/speding-monthly-control/{month}/{year}", GetSpedingMonthlyControl)
                .RequireAuthorization();

            return group;
        }

        private static async Task<IResult> GetSpedingMonthlyControl(
            int month,
            int year,
            HttpContext context,
            IMediator mediator
        )
        {
            var userId = context.User.FindFirst("userId")?.Value;
            if(userId is null)
                return Results.Unauthorized();

            var response = await mediator.Send(new SpendingMonthlyControlQuery
            {
                UserId = Guid.Parse(userId),
                Month = month,
                Year = year
            });

            return Results.Ok(response);
        }
    }
}