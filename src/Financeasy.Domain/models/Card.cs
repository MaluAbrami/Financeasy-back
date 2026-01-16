using System.ComponentModel.DataAnnotations.Schema;
using Financeasy.Domain.Enums;

namespace Financeasy.Domain.models
{
    [Table("card")]
    public class Card
    {
        [Column("id")]
        public Guid Id { get; set; }

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

        public Card(Guid bankAccountId, string name, decimal creditLimit, decimal availableLimit, int closingDay, int dueDay)
        {
            Id = Guid.NewGuid();
            BankAccountId = bankAccountId;
            Name = name;
            CreditLimit = creditLimit;
            AvailableLimit = availableLimit;
            ClosingDay = closingDay;
            DueDay = dueDay;
        }
    }
}