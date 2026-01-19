using Financeasy.Application.UseCases.TransactionCases.CreateTransaction;
using Financeasy.Application.UseCases.TransactionCases.DeleteTransaction;
using Financeasy.Domain.DTO.Transaction;
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
    }
}