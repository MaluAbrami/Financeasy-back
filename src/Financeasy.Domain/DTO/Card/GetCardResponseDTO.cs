namespace Financeasy.Domain.DTO.Card
{
    public record GetCardResponseDTO
    {
        public Guid Id { get; set; }
        public string BankAccountName { get; set; }
        public string Name { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal UsedLimit { get; set; }
        public int ClosingDay { get; set; }
        public int DueDay { get; set; }
    }
}