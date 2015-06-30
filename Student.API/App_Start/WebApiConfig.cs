using System.Net.Http.Headers;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using Student.DependencyResolution;

namespace Student.API
{
    public class WebApiConfig
    {
        public static HttpConfiguration Register()
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json-patch+json"));
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new ApiContractResolver();

            config.DependencyResolver = new WindsorHttpDependencyResolver(IocRegistration.IoCContainer);

            return config;
        }
    }
}