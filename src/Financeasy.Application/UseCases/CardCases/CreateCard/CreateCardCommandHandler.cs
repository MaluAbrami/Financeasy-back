using Financeasy.Domain.Enums;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using MediatR;

namespace Financeasy.Application.UseCases.CardCases.CreateCard
{
    public class CreateCardCommandHandler : IRequestHandler<CreateCardCommand, Guid>
    {
        private readonly ICardRepository _cardRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCardCommandHandler(ICardRepository cardRepository, IUnitOfWork unitOfWork)
        {
            _cardRepository = cardRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateCardCommand request, CancellationToken cancellationToken)
        {
            var nameDuplicate = await _cardRepository.FindAsync(x => x.Name == request.Name && x.BankAccountId == request.BankAccountId, cancellationToken);
            if(nameDuplicate.Any())
                throw new ArgumentException("Já existe um cartão com esse nome no mesmo banco.");

            Card newCard = new Card(
                request.UserId,
                request.BankAccountId,
                request.Name,
                request.CreditLimit,
                request.ClosingDay,
                request.DueDay
            );

            await _cardRepository.AddAsync(newCard, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return newCard.Id;
        }
    }
}