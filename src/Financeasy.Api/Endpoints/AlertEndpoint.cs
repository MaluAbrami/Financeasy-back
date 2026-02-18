using Financeasy.Application.UseCases.AlertsCases.CreateAlert;
using Financeasy.Application.UseCases.AlertsCases.DeleteAlert;
using Financeasy.Application.UseCases.AlertsCases.GetAllAlertsByMonth;
using Financeasy.Application.UseCases.AlertsCases.PayAlert;
using Financeasy.Domain.DTO.Alert;
using Financeasy.Domain.DTO.Pagination;
using MediatR;

namespace Financeasy.Api.Endpoints
{
    public static class AlertEndpoint
    {
        public static RouteGroupBuilder MapAlerts(this RouteGroupBuilder group)
        {
            group.MapPost("", CreateAlert)
                .RequireAuthorization();

            group.MapDelete("", DeleteAlert)
                .RequireAuthorization();

            group.MapGet("/all-by-month", GetAllAlertsByMonth)
                .RequireAuthorization();

            group.MapPatch("/pay-alert", PayAlert)
                .RequireAuthorization();

            return group;
        }

        private static async Task<IResult> CreateAlert(
            CreateAlertDTO request,
            IMediator mediator,
            HttpContext context
        )
        {
            var userId = context.User.FindFirst("userId")?.Value;

            if(userId is null)
                return Results.Unauthorized();

            await mediator.Send(new CreateAlertCommand
            {
                UserId = Guid.Parse(userId),
                CategoryId = request.CategoryId,
                RecurrenceType = request.RecurrenceType,
                DueDate = request.DueDate,
                ExpectedAmount = request.ExpectedAmount
            });

            return Results.Created();
        }

        private static async Task<IResult> DeleteAlert(
            Guid id,
            IMediator mediator,
            HttpContext context
        )
        {
            var userId = context.User.FindFirst("userId")?.Value;

            if(userId is null)
                return Results.Unauthorized();

            await mediator.Send(new DeleteAlertCommand
            {
                UserId = Guid.Parse(userId),
                Id = id
            });

            return Results.NoContent();
        }

        private static async Task<IResult> GetAllAlertsByMonth(
            int month,
            int year,
            IMediator mediator,
            HttpContext context,
            int page = 1,
            int pageSize = 10
        )
        {
            var userId = context.User.FindFirst("userId")?.Value;

            if(userId is null)
                return Results.Unauthorized();

            var response = await mediator.Send(new GetAllAlertsByMonth
            {
                Month = month,
                Year = year,
                UserId = Guid.Parse(userId),
                Pagination = new PaginationRequestBase
                {
                    Page = page,
                    PageSize = pageSize
                }
            });

            return Results.Ok(response);
        }

        private static async Task<IResult> PayAlert(
            Guid id,
            IMediator mediator,
            HttpContext context
        )
        {
            var userId = context.User.FindFirst("userId")?.Value;

            if(userId is null)
                return Results.Unauthorized();

            await mediator.Send(new PayAlertCommand
            {
                UserId = Guid.Parse(userId),
                Id = id
            });

            return Results.Ok("Alerta de conta paga para o mês em questão");
        }
    }
}