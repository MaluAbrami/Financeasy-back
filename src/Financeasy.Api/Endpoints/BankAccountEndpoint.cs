using Financeasy.Application.UseCases.BankAccountCases.CreateBankAccount;
using Financeasy.Application.UseCases.BankAccountCases.DeleteBankAccount;
using Financeasy.Application.UseCases.BankAccountCases.GetAllBanksAccounts;
using Financeasy.Application.UseCases.BankAccountCases.UpdateAccountBalance;
using Financeasy.Domain.DTO.BankAccount;
using Financeasy.Domain.DTO.Pagination;
using Financeasy.Domain.Enums;
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

            group.MapGet("/get-all/{page}/{pageSize}/{orderBy}/{direction}", GetAllBanksAccounts)
                .RequireAuthorization();

            group.MapDelete("/{id}", DeleteBankAccount)
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

        private static async Task<IResult> GetAllBanksAccounts(
            HttpContext context, 
            IMediator mediator,
            int page = 1,
            int pageSize = 10,
            BankAccountOrderBy orderBy = BankAccountOrderBy.Balance,
            SortDirection direction = SortDirection.Asc)
        {
            var userId = context.User.FindFirst("userId")?.Value;

            if(userId is null)
                return Results.Unauthorized();

            var banksAccounts = await mediator.Send(new GetAllBanksAccountsQuery 
                { 
                    UserId = Guid.Parse(userId), 
                    Pagination = new PaginationRequestBase
                    {
                        Page = page,
                        PageSize = pageSize
                    },
                    OrderBy = orderBy,
                    Direction = direction
                } 
            );

            return Results.Ok(banksAccounts);
        }

        private static async Task<IResult> DeleteBankAccount(Guid id, HttpContext context, IMediator mediator)
        {
            var userId = context.User.FindFirst("userId")?.Value;

            if(userId is null)
                return Results.Unauthorized();

            await mediator.Send(new DeleteBankAccountCommand { UserId = Guid.Parse(userId), BankAccountId = id } );

            return Results.NoContent();
        }
    }
}