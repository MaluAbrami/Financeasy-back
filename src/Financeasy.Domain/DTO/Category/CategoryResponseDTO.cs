using Financeasy.Domain.Enums;

namespace Financeasy.Domain.DTO.Category
{
    public class CategoryResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public EntryType Type { get; set; }
        public RecurrenceType RecurrenceType { get; set; }
    }
}