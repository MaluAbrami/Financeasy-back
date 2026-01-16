using System.ComponentModel.DataAnnotations.Schema;

namespace Financeasy.Domain.models
{
    [Table("card_installment")]
    public class CardInstallment
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("card_purchase_id")]
        public Guid CardPurchaseId { get; set; }

        [Column("card_invoice_id")]
        public Guid CardInvoiceId { get; set; }

        [Column("number")]
        public int Number { get; set; }

        [Column("total_installments")]
        public int TotalInstallments { get; set; }

        [Column("amount")]
        public decimal Amount { get; set; }

        [Column("paid")]
        public bool Paid { get; set; }

        public CardInstallment()
        {
        }

        public CardInstallment(Guid cardPurchaseId, Guid cardInvoiceId, int number, int totalInstallments, decimal amount)
        {
            Id = Guid.NewGuid();
            CardPurchaseId = cardPurchaseId;
            CardInvoiceId = cardInvoiceId;
            Number = number;
            TotalInstallments = totalInstallments;
            Amount = amount;
            Paid = false;
        }
    }
}