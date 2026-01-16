using System.ComponentModel.DataAnnotations.Schema;
using Financeasy.Domain.Enums;

namespace Financeasy.Domain.models
{
    [Table("card")]
    public class Card
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("bank_account_id")]
        public Guid BankAccountId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("credit_limit")]
        public decimal CreditLimit { get; set; }

        [Column("available_limit")]
        public decimal AvailableLimit { get; set; }

        [Column("closing_day")]
        public int ClosingDay { get; set; }

        [Column("due_day")]
        public int DueDay { get; set; }

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
            AvailableLimit = creditLimit;
            ClosingDay = closingDay;
            DueDay = dueDay;
        }

        public void DecreaseAvailableLimit(decimal value)
        {
            AvailableLimit -= value;
        }
    }
}