using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using MediatR;

namespace Financeasy.Application.UseCases.CardCases.UpdateCard
{
    public class UpdateCardHandler : IRequestHandler<UpdateCardCommand, UpdateCardCommandRespose>
    {
        private readonly ICardRepository _cardRepository;
        private readonly ICardInvoiceRepository _invoiceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCardHandler(
            ICardRepository cardRepository,
            ICardInvoiceRepository invoiceRepository,
            IUnitOfWork unitOfWork
        )
        {
            _cardRepository = cardRepository;
            _invoiceRepository = invoiceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateCardCommandRespose> Handle(UpdateCardCommand request, CancellationToken cancellationToken)
        {
            var cardExist = await _cardRepository.GetByIdAsync(request.CardId, cancellationToken);

            if(cardExist == null)
                throw new Exception("Cartão não foi encontrado");

            if(cardExist.UserId != request.UserId)
                throw new UnauthorizedAccessException("Usuário não é dono do cartão, portanto não tem acesso a realizar essa ação");

            if(!string.IsNullOrEmpty(request.Name))
                cardExist.Name = request.Name;
            
            if(request.CreditLimit > cardExist.CreditLimit)
                cardExist.CreditLimit = request.CreditLimit;
            
            if(request.ClosingDay != 0 && request.DueDay != 0)
            {
                cardExist.ClosingDay = request.ClosingDay;
                cardExist.DueDay = request.DueDay;

                var invoices = await _invoiceRepository.GetAllActiveInvoicesByCardIdAsync(request.CardId, DateTime.Now.Date, cancellationToken);                
                
                foreach(var invoice in invoices)
                {
                    var year = invoice.ClosingDate.Year;
                    var month = invoice.ClosingDate.Month;
                    var day = Math.Min(request.ClosingDay, DateTime.DaysInMonth(year, month));
                    invoice.ClosingDate = new DateTime(year, month, day, invoice.ClosingDate.Hour, invoice.ClosingDate.Minute, invoice.ClosingDate.Second, invoice.ClosingDate.Kind);
                    
                    // Se o dia de vencimento é menor que o de fechamento, deve ser no mês seguinte
                    var dueDateMonth = month;
                    var dueDateYear = year;
                    
                    if(request.DueDay < request.ClosingDay)
                    {
                        dueDateMonth = month + 1;
                        if(dueDateMonth > 12)
                        {
                            dueDateMonth = 1;
                            dueDateYear = year + 1;
                        }
                    }
                    
                    var dueDay = Math.Min(request.DueDay, DateTime.DaysInMonth(dueDateYear, dueDateMonth));
                    invoice.DueDate = new DateTime(dueDateYear, dueDateMonth, dueDay, invoice.DueDate.Hour, invoice.DueDate.Minute, invoice.DueDate.Second, invoice.DueDate.Kind);
                }
            }   

            _cardRepository.Update(cardExist);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new UpdateCardCommandRespose
            {
                Id = cardExist.Id,
                BankAccountId = cardExist.BankAccountId,
                Name = cardExist.Name,
                CreditLimit = cardExist.CreditLimit,
                ClosingDay = cardExist.ClosingDay,
                DueDay = cardExist.DueDay,
                IsActive = cardExist.IsActive
            };
        }
    }
}