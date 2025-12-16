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

        [Column("description")]
        public string? Description { get; set; } = string.Empty;

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("category_name")]
        public string CategoryName { get; set; }

        [Column("type")]
        public EntryType Type { get; set; }

        [Column("is_fixed")]
        public bool IsFixed { get; set; }

        [Column("source")]
        public SourceType Source { get; set; }

        public FinancialEntry()
        {
            
        }

        public FinancialEntry(Guid userId, decimal amount, string? description, DateTime date, Category category, SourceType source)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Amount = amount;
            CategoryName = category.Name;
            Date = date;
            Type = category.Type;
            IsFixed = category.IsFixed;
            Source = source;

            if (description is not null)
                Description = description;
        }

        public void Update(FinancialEntryUpdateDTO req)
        {
            if (req.Amount.HasValue && Amount != req.Amount.Value)
                Amount = req.Amount.Value;

            if (!string.IsNullOrWhiteSpace(req.Description) && Description != req.Description)
                Description = req.Description;

            if (req.Date.HasValue && Date != req.Date.Value)
                Date = req.Date.Value;
        }
    }
}