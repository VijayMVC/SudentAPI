using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.DataAccess.Repositories
{
    class NHibernateSessionProvider
    {
        public static IDictionary<string, SessionProvider> SessionFactories { get; private set; }
        public static void AddSessionFactoryConfiguration(string key, SessionProvider configuration)
        {
            if (SessionFactories == null)
                SessionFactories = new Dictionary<string, SessionProvider>();

            if (!SessionFactories.ContainsKey(key))
                SessionFactories.Add(key, configuration);
        }
    }
}
