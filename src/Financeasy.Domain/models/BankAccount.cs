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

        [Column("bank")]
        public string Bank { get; set; }

        [Column("balance")]
        public decimal Balance { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("deleted_at")]
        public DateTime DeletedAt { get; set; }

        public BankAccount()
        {
        }

        public BankAccount(Guid userId, string bank, decimal balance)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Bank = bank;
            Balance = balance;
            IsActive = true;
            DeletedAt = new DateTime();
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