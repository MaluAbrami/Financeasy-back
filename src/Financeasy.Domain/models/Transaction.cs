using System.ComponentModel.DataAnnotations.Schema;
using Financeasy.Domain.Enums;

namespace Financeasy.Domain.models
{
    [Table("transaction")]
    public class Transaction
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("bank_account_id")]
        public Guid BankAccountId { get; set; }

        [Column("category_id")]
        public Guid CategoryId { get; set; }

        [Column("type")]
        public EntryType Type { get; set; }

        [Column("payment_method")]
        public PaymentMethod PaymentMethod { get; set; }

        [Column("amount")]
        public decimal Amount { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }


        public Category Category { get; set; } = null!;
        public BankAccount? BankAccount { get; set; }

        public Transaction()
        {
        }

        public Transaction(Guid userId, Guid bankAccountId, Guid categoryId, EntryType type, PaymentMethod paymentMethod, decimal amount, DateTime date, string description)
        {
            Id = Guid.NewGuid();
            Type = type;
            PaymentMethod = paymentMethod;
            UserId = userId;
            BankAccountId = bankAccountId;
            CategoryId = categoryId;
            Amount = amount;
            Date = date;
            Description = description;
            CreatedAt = DateTime.Now;
        }
    }
}