using Financeasy.Application.UseCases.TransactionCases.CreateTransaction;
using Financeasy.Application.UseCases.TransactionCases.DeleteTransaction;
using Financeasy.Application.UseCases.TransactionCases.GetAllTransactions;
using Financeasy.Domain.DTO.Pagination;
using Financeasy.Domain.DTO.Transaction;
using Financeasy.Domain.Enums;
using MediatR;

namespace Financeasy.Api.Endpoints
{
    public static class TransactionEndpoint
    {
        public static RouteGroupBuilder MapTransaction(this RouteGroupBuilder group)
        {
            group.MapPost("", CreateTransaction)
                .RequireAuthorization();

            group.MapDelete("/{transactionId}", DeleteTransaction)
                .RequireAuthorization();

            group.MapGet("/get-all/{page}/{pageSize}/{orderBy}/{direction}", GetAllTransactions)
                .RequireAuthorization();

            return group;
        }

        private static async Task<IResult> CreateTransaction(CreateTransactionRequest request, HttpContext context, IMediator mediator)
        {
            var userId = context.User.FindFirst("userId")?.Value;
            if(userId is null)
                return Results.Unauthorized();

            await mediator.Send(new CreateTransactionCommand 
                { 
                    UserId = Guid.Parse(userId), 
                    BankAccountId = request.BankAccountId,
                    CategoryId = request.CategoryId,
                    PaymentMethod = request.PaymentMethod,
                    Amount = request.Amount,
                    Date = request.Date,
                    Description = request.Description
                }
            );

            return Results.Created();
        }

        private static async Task<IResult> DeleteTransaction(Guid transactionId, HttpContext context, IMediator mediator)
        {
            var userId = context.User.FindFirst("userId")?.Value;
            if(userId is null)
                return Results.Unauthorized();

            await mediator.Send(new DeleteTransactionCommand { UserId = Guid.Parse(userId), TransactionId = transactionId });

            return Results.NoContent();
        }

        private static async Task<IResult> GetAllTransactions(
            HttpContext context, 
            IMediator mediator,
            int page = 1, 
            int pageSize = 10, 
            TransactionOrderBy orderBy = TransactionOrderBy.Date, 
            SortDirection direction = SortDirection.Asc
            )
        {
            var userId = context.User.FindFirst("userId")?.Value;
            if(userId is null)
                return Results.Unauthorized();

            var transactions = await mediator.Send(new GetAllTransactionsQuery 
            { 
                UserId = Guid.Parse(userId), 
                Pagination = new PaginationRequestBase
                {
                    Page = page,
                    PageSize = pageSize
                },
                OrderBy = orderBy,
                Direction = direction
            });

            return Results.Ok(transactions);
        }
    }
}