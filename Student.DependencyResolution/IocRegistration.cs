﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Security;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Student.DataAccess.Repositories;
using Student.Domain.Repositories;

namespace Student.DependencyResolution
{
    public class IocRegistration : IDisposable
    {
        private const String ConnectionString = "StudentAPI";

        public static IWindsorContainer IoCContainer { get; private set; }
        public static IRepositoryInit RepositoryInitialization { get; set; }

        public IocRegistration()
        {
            if (IoCContainer == null)
                IoCContainer = new WindsorContainer();

            Register();
        }

        private void Register()
        {
            #region Data Registration

            IoCContainer.Register(Component.For<IRepositoryInit>().ImplementedBy<RepositoryInit>()
                .LifeStyle.Transient);
            IoCContainer.Register(Component.For<IRepositoryProvider>().ImplementedBy<RepositoryProvider>()
                .LifeStyle.Transient);

            #endregion

            #region Services

            #endregion

            #region Repository Setup

            RepositoryInitialization = IoCContainer.Resolve<IRepositoryInit>();
            var assemblies = new List<Assembly>
            {
                Assembly.Load(new AssemblyName("Student.DataAccess"))
            };
            if (RepositoryInitialization != null)
            {
                var connectionString = ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString;

                RepositoryInitialization.RepositoryKey = ConnectionString;
                RepositoryInitialization.InitDataContext(connectionString, assemblies);
            }

            #endregion

            #region Controller Registration

            IoCContainer.Register(Classes.FromAssembly(Assembly.Load("Student.API")).BasedOn<IController>().Configure(c => c.LifestylePerWebRequest()));
            IoCContainer.Register(Classes.FromAssembly(Assembly.Load("Student.API")).BasedOn<IHttpController>().LifestyleScoped());

            #endregion
        }

        public void Dispose()
        {
            IoCContainer.Dispose();
        }
    }
}