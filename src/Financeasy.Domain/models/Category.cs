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
        public required string Name { get; set; }

        [Column("type")]
        public EntryType Type { get; set; }

        [Column("is_fixed")]
        public bool IsFixed { get; set; }

        [Column("recurrence")]
        public int? Recurrence { get; set; }

        public Category(string name, EntryType type, bool isFixed, int? recurrence)
        {
            Name = name;
            Type = type;
            IsFixed = isFixed;

            if (isFixed && recurrence is null)
                throw new ArgumentException("Recurrence é obrigatória para categorias fixas");

            if (!isFixed && recurrence is not null)
                throw new ArgumentException("Categorias não fixas não possuem recorrência");

            Recurrence = recurrence;
        }
    }
}