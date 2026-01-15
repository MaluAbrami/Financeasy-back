using System.ComponentModel.DataAnnotations.Schema;
using Financeasy.Domain.Enums;

namespace Financeasy.Domain.models
{
    [Table("transaction")]
    public class Transaction
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("payment_method")]
        public PaymentMethod PaymentMethod { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("bank_account_id")]
        public Guid BankAccountId { get; set; }

        [Column("card_id")]
        public Guid CardId { get; set; }

        [Column("category_id")]
        public Guid CategoryId { get; set; }

        // PROPRIEDADES DE NAVEGAÇÃO
        public Category Category { get; set; } = null!;
        public BankAccount? BankAccount { get; set; }
        public Card? Card { get; set; }

        [Column("amount")]
        public decimal Amount { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        public Transaction()
        {
        }

        public Transaction(PaymentMethod paymentMethod, Guid userId, Guid bankAccountId, Guid cardId, Guid categoryId, decimal amount, DateTime date, string description)
        {
            Id = Guid.NewGuid();
            PaymentMethod = paymentMethod;
            UserId = userId;
            BankAccountId = bankAccountId;
            CardId = cardId;
            CategoryId = categoryId;
            Amount = amount;
            Date = date;
            Description = description;
        }
    }
}