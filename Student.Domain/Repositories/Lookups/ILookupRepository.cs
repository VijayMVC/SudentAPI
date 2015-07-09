using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Student.Domain.Domain.Lookups;

namespace Student.Domain.Repositories.Lookups
{
    public interface ILookupRepository : IRepositoryProvider
    {
        T Find<T>(String shortDescription) where T : LookupBase;
    }
}
