using Financeasy.Application.UseCases.RecurrenceRuleCases.CreateRecurrenceRule;
using Financeasy.Application.UseCases.RecurrenceRuleCases.DeleteRecurrenceRule;
using Financeasy.Application.UseCases.RecurrenceRuleCases.GetAllRecurrencesByCategoryId;
using Financeasy.Application.UseCases.RecurrenceRuleCases.GetRecurrenceRuleById;
using Financeasy.Domain.DTO;
using MediatR;

namespace Financeasy.Api.Endpoints
{
    public static class RecurrenceRuleEndpoint
    {
        public static RouteGroupBuilder MapRecurrenceRule(this RouteGroupBuilder group)
        {
            group.MapPost("", CreateRecurrenceRule)
                .RequireAuthorization();

            group.MapGet("/all-by-category-id/{categoryId}", GetAllRecurrencesByCategoryId)
                .RequireAuthorization();

            group.MapGet("/{id}", GetRecurrenceById)
                .RequireAuthorization();

            group.MapDelete("/{id}", DeleteRecurrence)
                .RequireAuthorization();

            return group;
        }

        private static async Task<IResult> CreateRecurrenceRule(CreateRecurrenceRequestDTO request, HttpContext context, IMediator mediator)
        {
            var userId = context.User.FindFirst("userId")?.Value;

            if(userId is null)
                return Results.Unauthorized();

            await mediator.Send(new CreateRecurrenceRuleCommand
            {
                UserId = Guid.Parse(userId),
                Recurrence = request
            });

            return Results.Ok();
        }

        private static async Task<IResult> GetAllRecurrencesByCategoryId(Guid categoryId, HttpContext context, IMediator mediator)
        {
            var userId = context.User.FindFirst("userId")?.Value;

            if(userId is null)
                return Results.Unauthorized();

            var response = await mediator.Send(new GetAllRecurrenceByCategoryId
            {
                UserId = Guid.Parse(userId),
                CategoryId = categoryId
            });

            return Results.Ok(response);
        }

        private static async Task<IResult> GetRecurrenceById(Guid id, HttpContext context, IMediator mediator)
        {
            var userId = context.User.FindFirst("userId")?.Value;

            if(userId is null)
                return Results.Unauthorized();

            var response = await mediator.Send(new GetRecurrenceRuleById
            {
                UserId = Guid.Parse(userId),
                Id = id
            });

            return Results.Ok(response);
        }

        private static async Task<IResult> DeleteRecurrence(Guid id, HttpContext context, IMediator mediator)
        {
            var userId = context.User.FindFirst("userId")?.Value;

            if(userId is null)
                return Results.Unauthorized();

            await mediator.Send(new DeleteRecurrenceRuleCommand
            {
                UserId = Guid.Parse(userId),
                Id = id
            });

            return Results.Ok();
        }
    }
}