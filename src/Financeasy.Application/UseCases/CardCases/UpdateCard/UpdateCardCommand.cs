using MediatR;

namespace Financeasy.Application.UseCases.CardCases.UpdateCard
{
    public record UpdateCardCommandRespose()
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required Guid BankAccountId { get; set; }
        public decimal CreditLimit { get; set; }
        public int ClosingDay { get; set; }
        public int DueDay { get; set; }
        public bool IsActive { get; set; }
    };

    public class UpdateCardCommand : IRequest<UpdateCardCommandRespose>
    {
        public required Guid UserId { get; set; }
        public required Guid CardId { get; set; }
        public string? Name { get; set; }
        public decimal CreditLimit { get; set; }
        public int ClosingDay { get; set; }
        public int DueDay { get; set; }
    }
}