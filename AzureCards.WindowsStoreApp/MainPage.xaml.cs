using Microsoft.Azure.AppService;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace AzureCards.WindowsStoreApp
{
    public sealed partial class MainPage : Page
    {
        // todo: setup the page
        private const string GATEWAY = "https://azurecardscfcb436d21d347cfb4937af5bd76cf5b.azurewebsites.net/";
        private const string API_APP = "https://microsoft-apiappc2cbb4399a2e4950aea4c4ca04232898.azurewebsites.net/";

        private IAppServiceClient _appServiceClient;
        private IAzureCardsClient _azureCards;
        private string _deckId;
        private const string AUTH_PROVIDER = "twitter";

        public MainPageViewModel ViewModel
        {
            get { return this.DataContext as MainPageViewModel; }
            set { this.DataContext = value; }
        }

        // todo: authenticate
        private async Task<IAppServiceUser> AuthenticateAsync()
        {
            await _appServiceClient.Logout();

            while (_appServiceClient.CurrentUser == null)
            {
                await _appServiceClient.LoginAsync(AUTH_PROVIDER, false);
            }

            return _appServiceClient.CurrentUser;
        }

        // todo: handle OnNavigated
        public MainPage()
        {
            this.InitializeComponent();
            this.ViewModel = new MainPageViewModel();

            _appServiceClient = new AppServiceClient(
                GATEWAY,
                new TokenExpiredHandler(this.AuthenticateAsync)
                );
        }

        protected async override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            await AuthenticateAsync();

            _azureCards = _appServiceClient.CreateAzureCardsClient(
                new Uri(API_APP),
                new TokenExpiredHandler(AuthenticateAsync)
                );

            RefreshCardDisplay();

            base.OnNavigatedTo(e);
        }

        // todo: refresh the card display
        private void RefreshCardDisplay()
        {
            var refreshCards = new Action(() =>
            {
                this.ViewModel.Cards.Clear();

                var response = _azureCards.Deck.Deal(_deckId, 5);

                foreach (var card in response.Cards)
                    this.ViewModel.Cards.Add(card.CreateViewModel());
            });

            var startNewDeck = new Action(() =>
            {
                _deckId = _azureCards.Deck.New();

                for (int i = 0; i < 10; i++)
                    _azureCards.Deck.Shuffle(_deckId);

                refreshCards();
            });

            if (string.IsNullOrEmpty(_deckId))
                startNewDeck();

            try
            {
                refreshCards();
            }
            catch
            {
                startNewDeck();
            }
        }

        private void DealTapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            RefreshCardDisplay();
        }
    }
}
