using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Financeasy.Domain.Enums;

namespace Financeasy.Domain.models
{
    [Table("category")]
    public class Category
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public EntryType Type { get; set; }

        public Category()
        {
            
        }

        public Category(Guid userId, string name, EntryType type)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Name = name;
            Type = type;
        }
    }
}