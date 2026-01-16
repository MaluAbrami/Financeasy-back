using System.ComponentModel.DataAnnotations.Schema;

namespace Financeasy.Domain.models
{
    [Table("card_invoice")]
    public class CardInvoice
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("card_id")]
        public Guid CardId { get; set; }

        [Column("closing_date")]
        public DateTime ClosingDate { get; set; }

        [Column("due_date")]
        public DateTime DueDate { get; set; }

        [Column("total_amount")]
        public decimal TotalAmount { get; set; }

        [Column("is_paid")]
        public bool IsPaid { get; set; }

        public ICollection<CardInstallment> Installments { get; set; }

        public CardInvoice()
        {
        }

        public CardInvoice(Guid cardId, DateTime closingDate, DateTime dueDate, decimal totalAmount)
        {
            Id = Guid.NewGuid();
            CardId = cardId;
            ClosingDate = closingDate;
            DueDate = dueDate;
            TotalAmount = totalAmount;
            IsPaid = false;
        }

        public void AddAmount(decimal amount)
        {
            TotalAmount += amount;
        }
    }
}