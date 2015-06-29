using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Student.DependencyResolution;

namespace Student.API.Components
{
    using AppFunc = Func<IDictionary<String, Object>, Task>;
    using OwinEnvironment = IDictionary<String, Object>;

    public class DependencyInjectionComponent
    {
        private AppFunc Next { get; set; }

        public DependencyInjectionComponent(AppFunc next)
        {
            Next = next;
        }

        public async Task Invoke(OwinEnvironment environment)
        {
            IocRegistration.RepositoryInitialization.BeginRequest();

            await Next(environment);

            IocRegistration.RepositoryInitialization.EndRequest();
        }
    }
}