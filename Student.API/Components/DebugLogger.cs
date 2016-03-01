using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Student.API.Components
{
    using System.Diagnostics;
    using AppFunc = Func<IDictionary<String, Object>, Task>;
    using OwinEnvironment = IDictionary<String, Object>;

    public class DebugLogger
    {
        private AppFunc Next { get; set; }

        public DebugLogger(AppFunc next)
        {
            Next = next;
        }

        public async Task Invoke(OwinEnvironment environment)
        {
            Debug.WriteLine("Requesting: " + environment["owin.RequestPath"]);

            await Next(environment);

            Debug.WriteLine("Response: " + environment["owin.ResponseStatusCode"]);
        }
    }
}