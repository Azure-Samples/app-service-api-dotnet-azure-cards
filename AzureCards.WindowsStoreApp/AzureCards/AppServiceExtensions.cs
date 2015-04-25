using System;
using System.Net.Http;
using Microsoft.Azure.AppService;

namespace AzureCards.WindowsStoreApp
{
    public static class AppServiceExtensions
    {
        public static AzureCardsClient Create(this IAppServiceClient client)
        {
            return new AzureCardsClient(client.CreateHandler());
        }

        public static AzureCardsClient Create(this IAppServiceClient client, params DelegatingHandler[] handlers)
        {
            return new AzureCardsClient(client.CreateHandler(handlers));
        }

        public static AzureCardsClient Create(this IAppServiceClient client, Uri uri, params DelegatingHandler[] handlers)
        {
            return new AzureCardsClient(uri, client.CreateHandler(handlers));
        }

        public static AzureCardsClient Create(this IAppServiceClient client, HttpClientHandler rootHandler, params DelegatingHandler[] handlers)
        {
            return new AzureCardsClient(rootHandler, client.CreateHandler(handlers));
        }
    }
}
