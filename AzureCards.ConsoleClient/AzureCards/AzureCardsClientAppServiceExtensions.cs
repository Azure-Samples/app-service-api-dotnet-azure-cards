using System;
using System.Net.Http;
using Microsoft.Azure.AppService;

namespace AzureCards.ConsoleClient
{
    public static class AzureCardsClientAppServiceExtensions
    {
        public static AzureCardsClient CreateAzureCardsClient(this IAppServiceClient client)
        {
            return new AzureCardsClient(client.CreateHandler());
        }

        public static AzureCardsClient CreateAzureCardsClient(this IAppServiceClient client, params DelegatingHandler[] handlers)
        {
            return new AzureCardsClient(client.CreateHandler(handlers));
        }

        public static AzureCardsClient CreateAzureCardsClient(this IAppServiceClient client, Uri uri, params DelegatingHandler[] handlers)
        {
            return new AzureCardsClient(uri, client.CreateHandler(handlers));
        }

        public static AzureCardsClient CreateAzureCardsClient(this IAppServiceClient client, HttpClientHandler rootHandler, params DelegatingHandler[] handlers)
        {
            return new AzureCardsClient(rootHandler, client.CreateHandler(handlers));
        }
    }
}
