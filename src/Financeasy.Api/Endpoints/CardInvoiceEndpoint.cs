using Financeasy.Application.UseCases.CardInvoiceCases.PayCardInvoiceCommand;
using Financeasy.Domain.DTO.CardInvoice;
using MediatR;

namespace Financeasy.Api.Endpoints
{
    public static class CardInvoiceEndpoint
    {
        public static RouteGroupBuilder MapCardInvoice(this RouteGroupBuilder group)
        {
            group.MapPost("/pay-card-invoice", PayCardInvoice)
                .RequireAuthorization();

            return group;
        }

        private static async Task<IResult> PayCardInvoice(PayCardInvoiceRequest request, HttpContext context, IMediator mediator)
        {
            var userId = context.User.FindFirst("userId")?.Value;
            if(userId is null)
                return Results.Unauthorized();

            await mediator.Send(new PayCardInvoiceCommand { UserId = Guid.Parse(userId), CardId = request.CardId, ClosingDate = request.ClosingDate });

            return Results.Ok();
        }
    }
}