using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Financeasy.Domain.Enums;

namespace Financeasy.Domain.models
{
    [Table("category")]
    public class Category
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("type")]
        public EntryType Type { get; set; }

        [Column("recurrence_type")]
        public RecurrenceType RecurrenceType { get; set; }

        public Category()
        {
            
        }

        public Category(Guid userId, string name, EntryType type, RecurrenceType recurrenceType)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Name = name;
            Type = type;
            RecurrenceType = recurrenceType;
        }
    }
}