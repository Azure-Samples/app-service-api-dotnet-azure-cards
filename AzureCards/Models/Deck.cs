using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AzureCards.Models
{
    [JsonObject(MemberSerialization.Fields)]
    public class Deck
    {
        public List<Card> RemainingCards { get; set; }

        [DebuggerStepThroughAttribute]
        public Deck()
        {
            RemainingCards = new List<Card>();

            foreach (var suit in Enum.GetNames(typeof(Suit)))
            {
                foreach (var face in Enum.GetNames(typeof(Face)))
                {
                    var cardToAdd = new Card
                    {
                        Face = (Face)Enum.Parse(typeof(Face), face),
                        Suit = (Suit)Enum.Parse(typeof(Suit), suit),
                    };

                    RemainingCards.Add(cardToAdd);
                }
            }
        }

        public Deck Shuffle()
        {
            var temp = new List<Card>();

            while (this.RemainingCards.Count > 0)
            {
                var r = new Random().Next(0, this.RemainingCards.Count - 1);
                temp.Add(this.RemainingCards[r]);
                this.RemainingCards.RemoveAt(r);
            }

            foreach (var x in temp)
                this.RemainingCards.Add(x);

            return this;
        }

        public static bool operator ==(Deck a, Deck b)
        {
            if ((object.Equals(a, null) && !object.Equals(b, null) || (object.Equals(b, null) && !object.Equals(a, null))))
                return false;
            if (object.Equals(a, null) && object.Equals(b, null))
                return true;

            if (a.RemainingCards.Count != b.RemainingCards.Count)
                return false;

            for (int i = 0; i < a.RemainingCards.Count; i++)
            {
                if (a.RemainingCards[i] != b.RemainingCards[i])
                    return false;
            }

            return true;
        }

        public static bool operator !=(Deck a, Deck b)
        {
            if ((object.Equals(a, null) && !object.Equals(b, null)) || (object.Equals(b, null) && !object.Equals(a, null)))
                return true;
            if (object.Equals(a, null) && object.Equals(b, null))
                return false;

            for (int i = 0; i < a.RemainingCards.Count; i++)
            {
                if (a.RemainingCards[i] != b.RemainingCards[i])
                    return true;
            }

            return false;
        }
    }
}
