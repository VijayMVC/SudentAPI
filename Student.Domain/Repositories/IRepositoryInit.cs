using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Student.Domain.Repositories
{
    public interface IRepositoryInit : IDisposable
    {
        string RepositoryKey { get; set; }
        void InitDataContext(string connectionString);
        void InitDataContext(string connectionString, List<Assembly> dataMappings);
        void BeginRequest(IsolationLevel isolation = IsolationLevel.ReadCommitted);
        void EndRequest();
    }
}
