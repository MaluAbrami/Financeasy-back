using Financeasy.Application.UseCases.CardInstallmentCases.GetAllInstallmentsByInvoice;
using Financeasy.Domain.DTO.Pagination;
using Financeasy.Domain.Enums;
using MediatR;

namespace Financeasy.Api.Endpoints
{
    public static class CardInstallmentsEndpoint
    {
        public static RouteGroupBuilder MapCardInstallment(this RouteGroupBuilder group)
        {
            return group;
        }

        private static async Task<IResult> GetAllInstallmentsByInvoice(
            Guid invoiceId,
            HttpContext context,
            IMediator mediator,
            int page = 1,
            int pageSize = 10,
            CardInstallmentOrderBy orderBy = CardInstallmentOrderBy.Amount,
            SortDirection direction = SortDirection.Asc
        )
        {
            var userId = context.User.FindFirst("userId")?.Value;

            if(userId is null)
                return Results.Unauthorized();

            var response = await mediator.Send(new GetAllInstallmentsByInvoiceQuery
            {
                InvoiceId = invoiceId,
                Pagination = new PaginationRequestBase
                {
                    Page = page,
                    PageSize = pageSize
                },
                OrderBy = orderBy,
                Direction = direction
            });

            return Results.Ok(response);
        }
    }
}