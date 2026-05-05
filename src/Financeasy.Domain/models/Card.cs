using System.ComponentModel.DataAnnotations.Schema;
using Financeasy.Domain.Enums;

namespace Financeasy.Domain.models
{
    [Table("card")]
    public class Card
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid BankAccountId { get; set; }
        public string Name { get; set; }
        public decimal CreditLimit { get; set; }
        public int ClosingDay { get; set; }
        public int DueDay { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public BankAccount BankAccount { get; set; }

        public Card()
        {
        }

        public Card(Guid userId, Guid bankAccountId, string name, decimal creditLimit, int closingDay, int dueDay)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            BankAccountId = bankAccountId;
            Name = name;
            CreditLimit = creditLimit;
            ClosingDay = closingDay;
            DueDay = dueDay;
            IsActive = true;
            CreatedAt = DateTime.Now;
            DeletedAt = null;
        }

        public void DisableCard()
        {
            IsActive = false;
            DeletedAt = DateTime.Now;
        }
    }
}