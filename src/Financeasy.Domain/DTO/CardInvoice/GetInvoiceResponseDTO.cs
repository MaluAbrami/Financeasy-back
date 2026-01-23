namespace Financeasy.Domain.DTO.CardInvoice
{
    public record GetInvoiceResponseDTO
    {
        public Guid Id { get; set; }
        public DateTime ClosingDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsPaid { get; set; }
    }
}