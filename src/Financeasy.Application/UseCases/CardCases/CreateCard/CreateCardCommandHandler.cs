using Financeasy.Domain.Enums;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using MediatR;

namespace Financeasy.Application.UseCases.CardCases.CreateCard
{
    public class CreateCardCommandHandler : IRequestHandler<CreateCardCommand, Guid>
    {
        private readonly ICardRepository _cardRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCardCommandHandler(ICardRepository cardRepository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _cardRepository = cardRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateCardCommand request, CancellationToken cancellationToken)
        {
            var nameDuplicate = await _cardRepository.FindAsync(x => x.Name == request.Name && x.BankAccountId == request.BankAccountId);
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

            Category newCategory = new Category(
                request.UserId,
                $"Fatura {request.Name}",
                EntryType.Expense,
                RecurrenceType.Monthly
            );

            await _categoryRepository.AddAsync(newCategory);
            await _cardRepository.AddAsync(newCard);
            await _unitOfWork.SaveChangesAsync();

            return newCard.Id;
        }
    }
}