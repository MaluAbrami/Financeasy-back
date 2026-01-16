using System.ComponentModel.DataAnnotations.Schema;

namespace Financeasy.Domain.models
{
    [Table("card_purchase")]
    public class CardPurchase
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("card_id")]
        public Guid CardId { get; set; }

        [Column("category_id")]
        public Guid CategoryId { get; set; }

        [Column("total_amount")]
        public decimal TotalAmount { get; set; }

        [Column("installments")]
        public int Installments { get; set; }

        [Column("purchase_date")]
        public DateTime PurchaseDate { get; set; }

        [Column("description")]
        public string? Description { get; set; } = string.Empty;

        public ICollection<CardInstallment> InstallmentsList { get; set; }

        public CardPurchase()
        {
        }

        public CardPurchase(Guid cardId, Guid categoryId, decimal totalAmount, int installments, DateTime purchaseDate, string? description)
        {
            Id = Guid.NewGuid();
            CardId = cardId;
            CategoryId = categoryId;
            TotalAmount = totalAmount;
            Installments = installments;
            PurchaseDate = purchaseDate;
            Description = description;
        }
    }
}