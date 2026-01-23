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
        private readonly ICardInvoiceRepository _cardInvoiceRepository;

        public GetAllInvoicesByCardHandler(ICardInvoiceRepository cardInvoiceRepository)
        {
            _cardInvoiceRepository = cardInvoiceRepository;
        }

        public async Task<GetAllInvoicesByCardResponse> Handle(GetAllInvoicesByCardQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<CardInvoice, object>> expression =
                request.OrderBy switch
                {
                    CardInvoiceOrderBy.ClosingDate => x => x.ClosingDate,
                    CardInvoiceOrderBy.DueDate => x => x.DueDate,
                    CardInvoiceOrderBy.TotalAmount => x => x.TotalAmount,
                    _ => x => x.TotalAmount
                };

            var invoices = await _cardInvoiceRepository.GetPagedAsync(
                x => x.CardId == request.CardId,
                expression,
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
                    TotalAmount = invoice.TotalAmount,
                    IsPaid = invoice.IsPaid
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