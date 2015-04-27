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
        private const string GW_URL = "YOUR GATEWAY URL";
        private const string URL_TOKEN = "#token=";

        [STAThread]
        static void Main(string[] args)
        {
            string deckId = string.Empty;

            Form frm = new Form();

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

                        var appServiceClient = new AppServiceClient(GW_URL);
                        appServiceClient.SetCurrentUser(userId, userToken);

                        var deckClient = appServiceClient.CreateAzureCardsClient();
                        deckId = deckClient.Deck.New();

                        frm.Close();
                    }
                };

            browser.Navigate(string.Format(@"{0}login/twitter", GW_URL));

            frm.Controls.Add(browser);
            frm.ShowDialog();

            Console.WriteLine(string.Format("Your new Deck ID is {0}", deckId));

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
    }
}
