using Financeasy.Domain.Enums;

namespace Financeasy.Domain.DTO
{
    public class CategoryResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public EntryType Type { get; set; }
        public bool IsFixed { get; set; }
        public int? Recurrence { get; set; }
    }
}