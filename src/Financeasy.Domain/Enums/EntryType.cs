using System.Text.Json.Serialization;

namespace Financeasy.Domain.Enums
{
    public enum EntryType
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        Income,
        [JsonConverter(typeof(JsonStringEnumConverter))]
        Expense
    }
}