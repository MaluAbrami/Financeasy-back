using Financeasy.Application.UseCases.DashboardsCases.GetBalanceEvolution;
using Financeasy.Application.UseCases.DashboardsCases.GetFinancialSummary;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using MySqlX.XDevAPI.Common;

namespace Financeasy.Api.Endpoints
{
    public static class DashboardsEndpoint
    {
        public static RouteGroupBuilder MapDashboards(this RouteGroupBuilder group)
        {
            group.MapGet("/financial-summary", GetFinancialSummary)
                .RequireAuthorization();
            group.MapGet("/balance-evolution/{initialYear}/{initialMonth}/{endYear}/{endMonth}", GetBalanceEvolution)
                .RequireAuthorization();

            return group;
        }

        private async static Task<IResult> GetFinancialSummary(HttpContext context, IMediator mediator)
        {
            var userId = context.User.FindFirst("userId")?.Value;
            if(userId is null)
                return Results.Unauthorized();

            var response = await mediator.Send(new GetFinancialSummary { UserId = Guid.Parse(userId) });
            return Results.Ok(response);
        }

        private async static Task<IResult> GetBalanceEvolution(int initialYear, int initialMonth, int endYear, int endMonth, HttpContext context, IMediator mediator)
        {
            var userId = context.User.FindFirst("userId")?.Value;
            if(userId is null)
                return Results.Unauthorized();

            var response = await mediator.Send(
                new GetBalanceEvolution 
                { 
                    UserId = Guid.Parse(userId), 
                    InitialYearComparison = initialYear,
                    InitialMonthComparison = initialMonth,
                    EndYearComparison = endYear,
                    EndMonthComparison = endMonth
                }
            );
            return Results.Ok(response);
        }
    }
}