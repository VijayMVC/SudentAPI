using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Collection;
using NHibernate.Proxy;

namespace Student.DataAccess
{
    public static class NHibernateContractResolver
    {
        public static Type GetSerializableType(Type objectType)
        {
            if (typeof (INHibernateProxy).IsAssignableFrom(objectType))
                return typeof (Object);
            if (typeof (AbstractPersistentCollection).IsAssignableFrom(objectType))
                return typeof (Object);

            return objectType;
        }
    }
}
