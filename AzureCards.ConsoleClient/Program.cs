using Microsoft.Azure.AppService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzureCards.ConsoleClient
{
    class Program
    {
        private const string GW_URL = "http://devintersectionseu66c15fe52d0b446c920739d03a993afa.azurewebsites.net/";
        private const string URL_TOKEN = "#token=";

        [STAThread]
        static void Main(string[] args)
        {
            var appServiceClient = new AppServiceClient(GW_URL);
            var deckClient = appServiceClient.CreateAzureCardsClient();
            var deckId = deckClient.Deck.New();

            Console.WriteLine(string.Format("Your new Deck ID is {0}", deckId));

            // shuffle the deck
            for(int i=0; i<10; i++)
                deckClient.Deck.Shuffle(deckId);

            // deal a hand and show it
            var hand = deckClient.Deck.Deal(deckId, 11);
            foreach (var card in hand.Cards)
                Console.WriteLine($"{card.Face} of {card.Suit}");

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }

        #region
        private static void AuthorizeClient(AppServiceClient appServiceClient)
        {
            Form frm = new Form();
            frm.Width = 640;
            frm.Height = 480;

            WebBrowser browser = new WebBrowser();
            browser.Dock = DockStyle.Fill;

            browser.DocumentCompleted += (sender, e) =>
            {
                if (e.Url.AbsoluteUri.IndexOf(URL_TOKEN) > -1)
                {
                    var encodedJson = e.Url.AbsoluteUri.Substring(e.Url.AbsoluteUri.IndexOf(URL_TOKEN) + URL_TOKEN.Length);
                    var decodedJson = Uri.UnescapeDataString(encodedJson);
                    var result = JsonConvert.DeserializeObject<dynamic>(decodedJson);
                    string userId = result.user.userId;
                    string userToken = result.authenticationToken;

                    appServiceClient.SetCurrentUser(userId, userToken);

                    frm.Close();
                }
            };

            browser.Navigate(string.Format(@"{0}login/twitter", GW_URL));

            frm.Controls.Add(browser);
            frm.ShowDialog();
        }
        #endregion
    }
}
