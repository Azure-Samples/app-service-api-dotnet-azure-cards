
namespace AzureCards.Models
{
    public class Card
    {
        public Suit Suit { get; set; }

        public Face Face { get; set; }

        public override bool Equals(object obj)
        {
            Card card = obj as Card;
            return (card.Face == this.Face) && (card.Suit == this.Suit);
        }

        public static bool operator ==(Card a, Card b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Card a, Card b)
        {
            return !a.Equals(b);
        }
    }
}
