using Financeasy.Application.UseCases.BankAccountCases.CreateBankAccount;
using Financeasy.Application.UseCases.BankAccountCases.UpdateAccountBalance;
using Financeasy.Domain.DTO.BankAccount;
using MediatR;

namespace Financeasy.Api.Endpoints
{
    public static class BankAccountEndpoint
    {
        public static RouteGroupBuilder MapBankAccounts(this RouteGroupBuilder group)
        {
            group.MapPost("", CreateBankAccount)
                .RequireAuthorization();

            group.MapPatch("/update-account-balance", UpdateAccountBalance)
                .RequireAuthorization();

            return group;
        }

        private static async Task<IResult> CreateBankAccount(CreateBankAccountRequest request, HttpContext context, IMediator mediator)
        {
            var userId = context.User.FindFirst("userId")?.Value;

            if(userId is null)
                return Results.Unauthorized();

            await mediator.Send(new CreateBankAccountCommand { UserId = Guid.Parse(userId), Bank = request.Bank, Balance = request.Balance } );

            return Results.Created();
        }

        private static async Task<IResult> UpdateAccountBalance(UpdateAccountBalanceRequest request, HttpContext context, IMediator mediator)
        {
            var userId = context.User.FindFirst("userId")?.Value;

            if(userId is null)
                return Results.Unauthorized();

            await mediator.Send(new UpdateAccountBalance { UserId = Guid.Parse(userId), Id = request.Id, Balance = request.Balance } );

            return Results.Ok();
        }
    }
}