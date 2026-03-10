using System.ComponentModel.DataAnnotations.Schema;

namespace Financeasy.Domain.models
{
    [Table("card_invoice")]
    public class CardInvoice
    {
        public Guid Id { get; set; }
        public Guid CardId { get; set; }
        public DateTime ClosingDate { get; set; }

        public DateTime DueDate { get; set; }

        public ICollection<CardInstallment> Installments { get; set; }

        public CardInvoice()
        {
        }

        public CardInvoice(Guid cardId, DateTime closingDate, DateTime dueDate)
        {
            Id = Guid.NewGuid();
            CardId = cardId;
            ClosingDate = closingDate;
            DueDate = dueDate;
        }
    }
}