using System.Web.Http;
using Swashbuckle.Application;
using WebActivatorEx;
using AzureCards;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace AzureCards
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c => c.SingleApiVersion("v1", "AzureCards"))
                .EnableSwaggerUi(c => { });
        }
    }
}