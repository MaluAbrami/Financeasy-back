namespace Financeasy.Domain.DTO.CardInstallment
{
    public record GetInstallmentResponseDTO
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public string PurchaseDescription { get; set; }
        public string NumberInstallment { get; set; }
        public decimal Amount { get; set; }
        public bool Paid { get; set; }
    }
}