namespace Financeasy.Domain.DTO.BankAccount
{
    public record GetBankAccountDTO
    {
        public Guid Id { get; set; }
        public string Bank { get; set; }
        public decimal Balance { get; set; }
    }
}