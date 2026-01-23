using System.Linq.Expressions;
using Financeasy.Domain.DTO.Pagination;
using Financeasy.Domain.Enums;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using MediatR;

namespace Financeasy.Application.UseCases.CardInstallmentCases.GetAllInstallmentsByInvoice
{
    public class GetAllInstallmentsByInvoiceHandler : IRequestHandler<GetAllInstallmentsByInvoiceQuery, GetAllInstallmentsByInvoiceResponse>
    {
        private readonly ICardInstallmentRepository _cardInstallmentRepository;

        public GetAllInstallmentsByInvoiceHandler(ICardInstallmentRepository cardInstallmentRepository)
        {
            _cardInstallmentRepository = cardInstallmentRepository;
        }

        public async Task<GetAllInstallmentsByInvoiceResponse> Handle(GetAllInstallmentsByInvoiceQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<CardInstallment, object>> expression =
                request.OrderBy switch
                {
                    CardInstallmentOrderBy.Amount => x => x.Amount,
                    CardInstallmentOrderBy.TotalInstallments => x => x.TotalInstallments,
                    _ => x => x.Amount
                };

            var installments = await _cardInstallmentRepository.GetPagedWithRelationsAsync(
                x => x.CardInvoiceId == request.InvoiceId,
                expression,
                request.Direction == SortDirection.Asc
                ? true
                : false,
                request.Pagination.Page,
                request.Pagination.PageSize,
                cancellationToken
            );

            return new GetAllInstallmentsByInvoiceResponse
            {
                Installments = installments.List,
                Pagination = new PaginationResponseBase
                {
                    Page = request.Pagination.Page,
                    PageSize = request.Pagination.PageSize,
                    TotalItems = installments.TotalItems,
                    TotalPages = (int)Math.Ceiling(
                        installments.TotalItems / (double)request.Pagination.PageSize
                    )
                }
            };
        }
    }
}