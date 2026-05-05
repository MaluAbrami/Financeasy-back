namespace Financeasy.Domain.DTO.Card
{
    public record CreateCardRequest
    {
        public Guid BankAccountId { get; set; }
        public required string Name { get; set; }
        public decimal CreditLimit { get; set; }
        public int ClosingDay { get; set; }
        public int DueDay { get; set; }
    }
}