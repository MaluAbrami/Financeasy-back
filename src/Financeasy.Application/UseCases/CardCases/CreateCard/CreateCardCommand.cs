using MediatR;

namespace Financeasy.Application.UseCases.CardCases.CreateCard
{
    public record CreateCardCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid BankAccountId { get; set; }
        public required string Name { get; set; }
        public decimal CreditLimit { get; set; }
        public int ClosingDay { get; set; }
        public int DueDay { get; set; }
    }
}