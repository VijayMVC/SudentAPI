using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;

namespace Student.DependencyResolution
{
    //public class WindsorMVCDependencyResolver : System.Web.Mvc.IDependencyResolver
    //{
    //    private readonly IWindsorContainer container;

    //    public WindsorMVCDependencyResolver(IWindsorContainer container)
    //    {
    //        this.container = container;
    //    }

    //    public object GetService(Type t)
    //    {
    //        var c = this.container.Kernel.HasComponent(t) ? this.container.Resolve(t) : null;
    //        if (c is Controller)
    //        {
    //            var cont = c as Controller;
    //            cont.ActionInvoker = container.Resolve<IActionInvoker>();
    //        }
    //        return c;
    //    }

    //    public IEnumerable<object> GetServices(Type t)
    //    {
    //        return this.container.ResolveAll(t).Cast<object>().ToArray();
    //    }
    //}
}
