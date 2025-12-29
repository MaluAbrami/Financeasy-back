using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Financeasy.Domain.models
{
    [Table("updates")]
    public class Update
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("update_date")]
        public DateTime UpdateDate { get; set; }

        [Column("total_recurrences_entrys")]
        public int TotalRecurrencesEntrys { get; set; }
    }
}