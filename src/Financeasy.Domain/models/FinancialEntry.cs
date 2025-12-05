using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Financeasy.Domain.Enums;

namespace Financeasy.Domain.models
{
    [Table("financial_entry")]
    public class FinancialEntry
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("amount")]
        public decimal Amount { get; set; }

        // TODO : desenvolver a entidade Category e seu CRUD e então substituir aqui e ver as propriedades que da para transferir a responsabilidade para a category depois (garantir que tenha o navigation property)
        [Column("category")]
        public string Category { get; set; }

        [Column("description")]
        public string? Description { get; set; } = string.Empty;

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("type")]
        public EntryType Type { get; set; }

        [Column("fixed")]
        public bool Fixed { get; set; }

        public FinancialEntry(Guid userId, decimal amount, string category, string? description, DateTime date, EntryType type, bool isFixed)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Amount = amount;
            Category = category;
            Date = date;
            Type = type;
            Fixed = isFixed;

            if(description is not null)
                Description = description;
        }
    }
}