using Financeasy.Application.UseCases.DashboardsCases.GetBalanceEvolution;
using Financeasy.Application.UseCases.DashboardsCases.GetFinancialSummary;
using Financeasy.Application.UseCases.DashboardsCases.GetSpendingByCategory;
using Financeasy.Application.UseCases.DashboardsCases.GetSpendingByMonth;
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
            group.MapGet("/spending-by-category/{category}", GetSpendingByCategory)
                .RequireAuthorization();
            group.MapGet("spending-by-month/{year}/{month}", GetSpendingByMonth)
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

        private async static Task<IResult> GetSpendingByCategory(string category, HttpContext context, IMediator mediator)
        {
            var userId = context.User.FindFirst("userId")?.Value;
            if(userId is null)
                return Results.Unauthorized();

            var response = await mediator.Send(new GetSpendingByCategory { UserId = Guid.Parse(userId), Category = category });

            return Results.Ok(response);
        }

        private async static Task<IResult> GetSpendingByMonth(int year, int month, HttpContext context, IMediator mediator)
        {
            var userId = context.User.FindFirst("userId")?.Value;
            if(userId is null)
                return Results.Unauthorized();

            var response = await mediator.Send(new GetSpendingByMonth { UserId = Guid.Parse(userId), Year = year, Month = month });

            return Results.Ok(response);
        }
    }
}