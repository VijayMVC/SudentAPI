using System.Web.Http;
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

            config.DependencyResolver = new WindsorHttpDependencyResolver(IocRegistration.IoCContainer);

            return config;
        }
    }
}