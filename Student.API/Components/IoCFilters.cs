using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Student.DependencyResolution;

namespace Student.API.Components
{
    public class IoCFilters : IFilter
    {
        public bool AllowMultiple { get; private set; }
        public static IocRegistration IoC { get; private set; }

        public IoCFilters()
        {
            AllowMultiple = true;

            if (IoC == null)
                IoC = new IocRegistration();
        }

        public void OnActionExecuting(HttpActionContext actionContext)
        {
            IocRegistration.RepositoryInitialization.BeginRequest();
        }

        public void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            IocRegistration.RepositoryInitialization.EndRequest();
        }
    }
}