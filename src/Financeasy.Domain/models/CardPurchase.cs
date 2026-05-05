using System.ComponentModel.DataAnnotations.Schema;

namespace Financeasy.Domain.models
{
    [Table("card_purchase")]
    public class CardPurchase
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CardId { get; set; }
        public Guid CategoryId { get; set; }
        public decimal TotalAmount { get; set; }
        public int Installments { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string? Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public ICollection<CardInstallment> InstallmentsList { get; set; }

        public Card Card { get; set; }
        public Category Category { get; set; }

        public CardPurchase()
        {
        }

        public CardPurchase(Guid userId, Guid cardId, Guid categoryId, decimal totalAmount, int installments, DateTime purchaseDate, string? description)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            CardId = cardId;
            CategoryId = categoryId;
            TotalAmount = totalAmount;
            Installments = installments;
            PurchaseDate = purchaseDate;
            Description = description;
            CreatedAt = DateTime.Now;
        }
    }
}