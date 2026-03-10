using System.ComponentModel.DataAnnotations.Schema;
using Financeasy.Domain.Enums;

namespace Financeasy.Domain.models
{
    [Table("transaction")]
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid BankAccountId { get; set; }
        public Guid CategoryId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }


        public Category Category { get; set; } = null!;
        public BankAccount? BankAccount { get; set; }

        public Transaction()
        {
        }

        public Transaction(Guid userId, Guid bankAccountId, Guid categoryId, PaymentMethod paymentMethod, decimal amount, DateTime date, string? description)
        {
            Id = Guid.NewGuid();
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