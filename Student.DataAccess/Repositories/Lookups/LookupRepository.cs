using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Criterion;
using Student.Domain.Domain.Lookups;
using Student.Domain.Repositories.Lookups;

namespace Student.DataAccess.Repositories.Lookups
{
    public class LookupRepository : RepositoryProvider, ILookupRepository
    {
        public T Find<T>(String shortDescription) where T : LookupBase 
        {
            var result = Session.CreateCriteria<T>()
                .Add(Restrictions.Eq("ShortDescription", shortDescription))
                .List<T>().FirstOrDefault();

            return result;
        }
    }
}
