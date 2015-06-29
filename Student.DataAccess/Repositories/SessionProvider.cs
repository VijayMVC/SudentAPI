using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Event;
using Student.DataAccess.Listeners;
using Configuration = NHibernate.Cfg.Configuration;

namespace Student.DataAccess.Repositories
{
    public class SessionProvider
    {
        protected readonly Object LockObject = new object();

        public string SessionKey { get; set; }
        public string TransactionKey { get; set; }
        public ISessionFactory SessionFactory { get; set; }
        public bool Initialized { get; set; }
        public List<Assembly> RegisterAssemblies { get; set; }
        private Configuration NhibenrnateConfiguration { get; set; }

        public SessionProvider() { }

        public SessionProvider(List<Assembly> registerAssemblies)
        {
            RegisterAssemblies = registerAssemblies;
        }

        public void Initialize(String connectionString)
        {
            SessionKey = "SESSION" + connectionString;
            TransactionKey = "TRANS" + connectionString;

            try
            {
                if (!Initialized && SessionFactory == null)
                {
                    lock (LockObject)
                    {
                        var appId = ConfigurationManager.AppSettings["AppId"];
                        if (!String.IsNullOrEmpty(appId))
                        {
                            if (connectionString.EndsWith(";"))
                                connectionString = connectionString.Substring(0, connectionString.Length - 1);

                            connectionString = String.Format("{0};Application Name='AppId={1}'", connectionString, appId);
                        }

                        var config = Fluently.Configure();
                        config.ExposeConfiguration(c => c.SetProperty("connection.connection_string", connectionString));

                        foreach (var a in RegisterAssemblies)
                        {
                            config.Mappings(m => m.FluentMappings.AddFromAssembly(a));
                        }

                        config.ExposeConfiguration(cfg =>
                        {
                            cfg.EventListeners.PreInsertEventListeners = new IPreInsertEventListener[] { new Insert() };
                            cfg.EventListeners.PreUpdateEventListeners = new IPreUpdateEventListener[] { new Update() };
                        });

                        NhibenrnateConfiguration = config.BuildConfiguration();
                        SessionFactory = config.BuildSessionFactory();
                    }

                    Initialized = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("NHibernate initialization failed", ex);
            }
        }
    }
}
