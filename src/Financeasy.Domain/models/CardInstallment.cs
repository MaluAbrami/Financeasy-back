using System.ComponentModel.DataAnnotations.Schema;

namespace Financeasy.Domain.models
{
    [Table("card_installment")]
    public class CardInstallment
    {
        public Guid Id { get; set; }
        public Guid CardPurchaseId { get; set; }
        public Guid CardInvoiceId { get; set; }
        public int Number { get; set; }
        public decimal Amount { get; set; }
        public bool Paid { get; set; }

        public CardPurchase CardPurchase { get; set; }

        public CardInstallment()
        {
        }

        public CardInstallment(Guid cardPurchaseId, Guid cardInvoiceId, int number, decimal amount)
        {
            Id = Guid.NewGuid();
            CardPurchaseId = cardPurchaseId;
            CardInvoiceId = cardInvoiceId;
            Number = number;
            Amount = amount;
            Paid = false;
        }
    }
}