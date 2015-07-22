using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Owin;

namespace Student.API.Components
{
    using AppFunc = Func<IDictionary<String, Object>, Task>;
    using OwinEnvironment = IDictionary<String, Object>;

    public static class AppBuilderExtensions
    {
        public static void UseDependencyInjection(this IAppBuilder app)
        {
            app.Use<DependencyInjectionComponent>();
        }

        public static void UseDebugLogging(this IAppBuilder app)
        {
            app.Use<DebugLogger>();
        }
    }
}