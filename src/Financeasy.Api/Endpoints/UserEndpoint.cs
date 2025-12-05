using Financeasy.Application.UseCases.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Financeasy.Api.Endpoints
{
    public static class UserEndpoint
    {
        public static RouteGroupBuilder MapUserEndpoints(this RouteGroupBuilder group)
        {
            group.MapPost("/register", RegisterUser);
            group.MapPost("/login", Login);
            group.MapGet("/{email}", GetUserByEmail);
            group.MapPut("", UpdateUser);
            group.MapDelete("/{id}", DeleteUser);

            return group;
        }

        private static async Task<IResult> RegisterUser(RegisterUserCommand command, IMediator mediator)
        {
            var userId = await mediator.Send(command);

            return Results.Ok(userId);
        }

        private static async Task<IResult> Login(RegisterUserCommand command, IMediator mediator)
        {
            var userId = await mediator.Send(command);

            return Results.Ok(userId);
        }

        private static async Task<IResult> GetUserByEmail(string query, IMediator mediator)
        {
            var userId = await mediator.Send(query);

            return Results.Ok(userId);
        }

        private static async Task<IResult> UpdateUser(RegisterUserCommand command, IMediator mediator)
        {
            var userId = await mediator.Send(command);

            return Results.Ok(userId);
        }

        private static async Task<IResult> DeleteUser(Guid command, IMediator mediator)
        {
            var userId = await mediator.Send(command);

            return Results.Ok(userId);
        }
    }
}