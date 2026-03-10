using System.Linq.Expressions;
using Financeasy.Domain.DTO.CardInvoice;
using Financeasy.Domain.DTO.Pagination;
using Financeasy.Domain.Enums;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using MediatR;

namespace Financeasy.Application.UseCases.CardInvoiceCases.GetAllCardInvoicesByCard
{
    public class GetAllInvoicesByCardHandler : IRequestHandler<GetAllInvoicesByCardQuery, GetAllInvoicesByCardResponse>
    {
        private readonly ICardInvoiceDapperRepository _invoiceDapperRepository;

        public GetAllInvoicesByCardHandler(ICardInvoiceDapperRepository invoiceDapperRepository)
        {
            _invoiceDapperRepository = invoiceDapperRepository;
        }

        public async Task<GetAllInvoicesByCardResponse> Handle(GetAllInvoicesByCardQuery request, CancellationToken cancellationToken)
        {
            var invoices = await _invoiceDapperRepository.GetPagedWithRelationsByCardAsync(
                request.CardId,
                request.OrderBy.ToString(),
                request.Direction == SortDirection.Asc
                ? true
                : false,
                request.Pagination.Page,
                request.Pagination.PageSize,
                cancellationToken
            );

            List<GetInvoiceResponseDTO> listResponse = [];
            foreach (var invoice in invoices.List)
            {
                var invoiceDto = new GetInvoiceResponseDTO
                {
                    Id = invoice.Id,
                    ClosingDate = invoice.ClosingDate,
                    DueDate = invoice.DueDate,
                    TotalAmount = invoice.TotalAmount
                };

                listResponse.Add(invoiceDto);
            }

            return new GetAllInvoicesByCardResponse
            {
                Invoices = listResponse,
                Pagination = new PaginationResponseBase
                {
                    Page = request.Pagination.Page,
                    PageSize = request.Pagination.PageSize,
                    TotalItems = invoices.TotalItems,
                    TotalPages = (int)Math.Ceiling(
                        invoices.TotalItems / (double)request.Pagination.PageSize
                    )    
                }
            };
        }
    }
}