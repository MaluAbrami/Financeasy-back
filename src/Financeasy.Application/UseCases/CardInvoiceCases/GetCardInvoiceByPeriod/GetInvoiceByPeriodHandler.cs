using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using MediatR;

namespace Financeasy.Application.UseCases.CardInvoiceCases.GetCardInvoiceByPeriod
{
    public class GetInvoiceByPeriodHandler : IRequestHandler<GetInvoiceByPeriodQuery, GetInvoiceByPeriodResponse?>
    {
        private readonly ICardInvoiceDapperRepository _invoiceDapperRepository;
        private readonly ICardDapperRepository _cardDapperRepository;

        public GetInvoiceByPeriodHandler(
            ICardInvoiceDapperRepository invoiceDapperRepository,
            ICardDapperRepository cardDapperRepository
            )
        {
            _invoiceDapperRepository = invoiceDapperRepository;
            _cardDapperRepository = cardDapperRepository;
        }

        public async Task<GetInvoiceByPeriodResponse?> Handle(GetInvoiceByPeriodQuery request, CancellationToken cancellationToken)
        {
            var card = await _cardDapperRepository.GetCardById(request.CardId, cancellationToken);
            if(card is null)
                throw new ArgumentException($"Não existe cartão com o id {request.CardId}");

            var dueDate = new DateTime(request.Year, request.Month, card.DueDay);

            var invoiceExist = await _invoiceDapperRepository.GetCardInvoiceByPeriod(request.CardId, dueDate, cancellationToken);

            if(invoiceExist is null)
                return null;

            var totalAmount = await _invoiceDapperRepository.GetTotalAmountByCardIdAndPeriod(card.Id, invoiceExist.ClosingDate, cancellationToken);

            return new GetInvoiceByPeriodResponse
            {
                ClosingDate = invoiceExist.ClosingDate,
                DueDate = invoiceExist.DueDate,
                IsPaid = invoiceExist.IsPaid,
                TotalAmount = totalAmount
            };
        }
    }
}