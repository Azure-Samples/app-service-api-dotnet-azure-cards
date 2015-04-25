using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AzureCards.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Suit
    {
        Spades = 4,
        Hearts = 3,
        Diamonds = 2,
        Clubs = 1
    }
}