namespace Financeasy.Domain.DTO.BankAccount
{
    public record GetPagedBankAccount
    {
        public List<GetBankAccountDTO> List { get; set; }
        public int TotalItems { get; set; }
    }
}