using System.Web.Http;
using System.Web.Http.Dispatcher;
using Tee.PerfectChannel.WebApi.IOC;

namespace Tee.PerfectChannel.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Services.Replace(typeof(IHttpControllerActivator), new CompositionRoot());

            FluentValidationConfig.SetupValidation(config);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}