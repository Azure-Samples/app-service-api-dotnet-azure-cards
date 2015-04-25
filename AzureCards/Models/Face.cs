using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AzureCards.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Face
    {
        Ace = 14,
        King = 13,
        Queen = 12,
        Jack = 11,
        Ten = 10,
        Nine = 9,
        Eight = 8,
        Seven = 7,
        Six = 6,
        Five = 5,
        Four = 4,
        Three = 3,
        Two = 2
    }
}
