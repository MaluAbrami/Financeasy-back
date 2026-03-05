namespace Financeasy.Domain.DTO.CardInstallment
{
    public record GetInstallmentResponseDTO
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public decimal Amount { get; set; }
        public bool Paid { get; set; }
    }
}