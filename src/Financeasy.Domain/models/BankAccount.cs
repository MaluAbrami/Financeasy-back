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

        public BankAccount()
        {
        }

        public BankAccount(Guid userId, string bank, decimal balance)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Bank = bank;
            Balance = balance;
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