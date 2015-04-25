using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureCards.Models
{
    public class DealResponseMessage
    {
        public DealResponseMessage()
        {
            this.Cards = new List<Card>();
        }

        public IEnumerable<Card> Cards { get; set; }
    }
}
