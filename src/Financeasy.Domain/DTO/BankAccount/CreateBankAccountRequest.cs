namespace Financeasy.Domain.DTO.BankAccount
{
    public record CreateBankAccountRequest
    {
        public string Bank { get; set; }
        public decimal Balance { get; set; }
    }
}