using Financeasy.Application.UseCases.FinancialEntryCases.CreateFinancialEntry;
using Financeasy.Application.UseCases.FinancialEntryCases.DeleteFinancialEntry;
using Financeasy.Application.UseCases.FinancialEntryCases.GetAllFinancialByUser;
using Financeasy.Application.UseCases.FinancialEntryCases.GetFinancialById;
using Financeasy.Application.UseCases.FinancialEntryCases.UpdateFinancialEntry;
using Financeasy.Domain.DTO;
using MediatR;

namespace Financeasy.Api.Endpoints
{
    public static class FinancialEntryEndpoint
    {
        public static RouteGroupBuilder MapFinancialEntryEndpoints(this RouteGroupBuilder group)
        {
            group.MapPost("", CreateFinancialEntry)
                .RequireAuthorization();

            group.MapGet("/{id}", GetFinancialEntryById)
                .RequireAuthorization();

            group.MapGet("/all-by-user", GetAllFinancialByUser)
                .RequireAuthorization();

            group.MapPut("", UpdateFinancialEntry)
                .RequireAuthorization();

            group.MapDelete("", DeleteFinancialEntry)
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

        private async static Task<IResult> GetFinancialEntryById(Guid id, HttpContext httpContext, IMediator mediator)
        {
            var userId = httpContext.User.FindFirst("userId")?.Value;

            if(userId is null)
                return Results.Unauthorized();

            FinancialResponseDTO financialEntry = await mediator.Send(new GetFinancialById { Id = id, UserId =  Guid.Parse(userId)});

            return Results.Ok(financialEntry);
        }

        private async static Task<IResult> GetAllFinancialByUser(HttpContext httpContext, IMediator mediator)
        {
            var userId = httpContext.User.FindFirst("userId")?.Value;

            if(userId is null)
                return Results.Unauthorized();

            GetAllFinancialResponse financialEntrys = await mediator.Send(new GetAllFinancialByUser { UserId =  Guid.Parse(userId)});

            return Results.Ok(financialEntrys);
        }

        private async static Task<IResult> UpdateFinancialEntry(UpdateFinancialEntryCommand command, HttpContext httpContext, IMediator mediator)
        {
            var userId = httpContext.User.FindFirst("userId")?.Value;

            if(userId is null)
                return Results.Unauthorized();

            command.UserId = Guid.Parse(userId);

            FinancialResponseDTO financialEntryUpdated = await mediator.Send(command);

            return Results.Ok(financialEntryUpdated);
        }

        private async static Task<IResult> DeleteFinancialEntry(Guid id, HttpContext httpContext, IMediator mediator)
        {
            var userId = httpContext.User.FindFirst("userId")?.Value;

            if(userId is null)
                return Results.Unauthorized();

            await mediator.Send(new DeleteFinancialEntryCommand { Id = id, UserId = Guid.Parse(userId) });

            return Results.NoContent();
        }
    }
}