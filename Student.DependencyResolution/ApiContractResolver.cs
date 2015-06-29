using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Student.DataAccess;

namespace Student.DependencyResolution
{
    public class ApiContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override List<MemberInfo> GetSerializableMembers(Type objectType)
        {
            return base.GetSerializableMembers(NHibernateContractResolver.GetSerializableType(objectType));
        }

        protected override JsonContract CreateContract(Type objectType)
        {
            return base.CreateContract(NHibernateContractResolver.GetSerializableType(objectType));
        }
    }
}
