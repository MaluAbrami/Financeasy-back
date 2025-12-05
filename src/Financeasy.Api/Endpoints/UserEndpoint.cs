using Financeasy.Application.UseCases.UserCases.DeleteUser;
using Financeasy.Application.UseCases.UserCases.GetUserByEmail;
using Financeasy.Application.UseCases.UserCases.Login;
using Financeasy.Application.UseCases.UserCases.RegisterUser;
using Financeasy.Application.UseCases.UserCases.UpdateUser;
using Financeasy.Domain.DTO;
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
            group.MapPut("", UpdateUser)
                .RequireAuthorization();
            group.MapDelete("", DeleteUser)
                .RequireAuthorization();

            return group;
        }

        private static async Task<IResult> RegisterUser(RegisterUserCommand command, IMediator mediator)
        {
            var userId = await mediator.Send(command);

            return Results.Ok(userId);
        }

        private static async Task<IResult> Login(LoginCommand command, IMediator mediator)
        {
            var token = await mediator.Send(command);

            return Results.Ok(token);
        }

        private static async Task<IResult> GetUserByEmail(string email, IMediator mediator)
        {
            var userId = await mediator.Send(new GetUserByEmailQuery { Email = email } );

            return Results.Ok(userId);
        }

        private static async Task<IResult> UpdateUser(UpdateUserRequestDTO userRequest, HttpContext context, IMediator mediator)
        {
            var userIdFromToken = context.User.FindFirst("userId")?.Value;

            if (userIdFromToken is null)
                return Results.Unauthorized();

            UpdateUserCommand command = new UpdateUserCommand();
            command.User = userRequest;

            command.UserId = Guid.Parse(userIdFromToken);
            var userId = await mediator.Send(command);

            return Results.Ok(userId);
        }

        private static async Task<IResult> DeleteUser(HttpContext context, IMediator mediator)
        {
            var userIdFromToken = context.User.FindFirst("userId")?.Value;

            if (userIdFromToken is null)
                return Results.Unauthorized();

            var command = new DeleteUserCommand(Guid.Parse(userIdFromToken));
            await mediator.Send(command);

            return Results.NoContent();
        }
    }
}