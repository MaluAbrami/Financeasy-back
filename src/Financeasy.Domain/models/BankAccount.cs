using System.ComponentModel.DataAnnotations.Schema;

namespace Financeasy.Domain.models
{
    [Table("bank_account")]
    public class BankAccount
    {
        [Column("id")]
        public Guid Id { get; set; }
        
        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("bank_name")]
        public string BankName { get; set; }

        [Column("balance")]
        public decimal Balance { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("deleted_at")]
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