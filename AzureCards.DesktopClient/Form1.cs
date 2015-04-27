using Microsoft.Azure.AppService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzureCards.DesktopClient
{
    public partial class Form1 : Form
    {
        private const string GW_URL = "YOUR GATEWAY URL";
        private const string URL_TOKEN = "#token=";

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _deckIdLabel.Visible = false;
            webBrowser1.Navigate(string.Format(@"{0}login/twitter", GW_URL));
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
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
                var deckId = deckClient.Deck.New();

                webBrowser1.Visible = false;
                _deckIdLabel.Text = string.Format("Your new Deck ID is {0}", deckId);
                _deckIdLabel.Visible = true;
            }
        }
    }
}
