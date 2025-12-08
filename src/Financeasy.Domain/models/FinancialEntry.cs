using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Financeasy.Domain.DTO;
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
        public bool IsFixed { get; set; }

        public FinancialEntry(Guid userId, decimal amount, string category, string? description, DateTime date, EntryType type, bool isFixed)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Amount = amount;
            Category = category;
            Date = date;
            Type = type;
            IsFixed = isFixed;

            if (description is not null)
                Description = description;
        }

        public void Update(FinancialEntryUpdateDTO req)
        {
            if (req.Amount.HasValue && Amount != req.Amount.Value)
                Amount = req.Amount.Value;

            if (!string.IsNullOrWhiteSpace(req.Category) && Category != req.Category)
                Category = req.Category;

            if (!string.IsNullOrWhiteSpace(req.Description) && Description != req.Description)
                Description = req.Description;

            if (req.Date.HasValue && Date != req.Date.Value)
                Date = req.Date.Value;

            if (req.Type.HasValue && Type != req.Type.Value)
                Type = req.Type.Value;

            if (req.IsFixed.HasValue && IsFixed != req.IsFixed.Value)
                IsFixed = req.IsFixed.Value;
        }
    }
}