using System.ComponentModel.DataAnnotations.Schema;

namespace Financeasy.Domain.models
{
    [Table("bank_account")]
    public class BankAccount
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string BankName { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public BankAccount()
        {
        }

        public BankAccount(Guid userId, string bankName, decimal balance)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            BankName = bankName;
            Balance = balance;
            IsActive = true;
            CreatedAt = DateTime.Now;
            DeletedAt = null;
        }

        public void DisableBankAccount()
        {
            IsActive = false;
            DeletedAt = DateTime.Now;
        }

        public void DecreaseBalance(decimal amount)
        {
            Balance -= amount;
        }

        public void IncreaseBalance(decimal amount)
        {
            Balance += amount;
        }
    }
}