namespace Financeasy.Domain.DTO.CardInvoice
{
    public record PayCardInvoiceRequest
    {
        public Guid CardId { get; set; }
        public DateTime ClosingDate { get; set; }
    }
}