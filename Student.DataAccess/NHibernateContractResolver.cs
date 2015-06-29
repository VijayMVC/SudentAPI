using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Proxy;

namespace Student.DataAccess
{
    public static class NHibernateContractResolver
    {
        public static Type GetSerializableType(Type objectType)
        {
            return typeof (INHibernateProxy).IsAssignableFrom(objectType) 
                ? objectType.BaseType 
                : objectType;
        }
    }
}
