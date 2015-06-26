using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.Owin;
using Owin;
using Student.API.Components;
using Student.DependencyResolution;

[assembly: OwinStartup(typeof(Student.API.Startup))]
namespace Student.API
{
    public class Startup
    {
        public static IocRegistration IoC { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            IoC = new IocRegistration();

            app.UseDependencyInjection();

            app.Use(async (environment, next) =>
            {
                Debug.WriteLine("Requesting: " + environment.Request.Path);

                await next();

                Debug.WriteLine("Response: " + environment.Response.StatusCode);
            });

            app.UseWebApi(WebApiConfig.Register());
        }
    }
}