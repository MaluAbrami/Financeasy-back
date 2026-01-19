namespace Financeasy.Domain.DTO.BankAccount
{
    public record UpdateAccountBalanceRequest
    {
        public Guid Id { get; set; }
        public decimal Balance { get; set; }
    }
}