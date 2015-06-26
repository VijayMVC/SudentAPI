using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;
using Castle.Windsor;

namespace Student.DependencyResolution
{
    public class WindsorHttpDependencyResolver : IDependencyResolver
    {
        private readonly IWindsorContainer _container;

        public WindsorHttpDependencyResolver(IWindsorContainer container)
        {
            if(container == null)
                throw new ArgumentNullException();

            _container = container;
        }

        public object GetService(Type serviceType)
        {
            return _container.Kernel.HasComponent(serviceType) ? _container.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.ResolveAll(serviceType).Cast<Object>().ToArray();
        }

        public IDependencyScope BeginScope()
        {
            return new WindsorHttpDependencyScope(_container);
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
