using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.Domain.Repositories
{
    public interface IRepositoryProvider
    {
        string RepositoryKey { get; set; }

        #region Sunflower Repository Methods
        /*
        void ForceFlush();
        T Get<T>(object id);
        T Load<T>(object id);
        void Evict<T>(T item);
        void Save<T>(T item);
        void Delete<T>(T item);
        IList<T> List<T>();
        T FindSingleByProperty<T>(string propertyName, object value) where T : class;
        IList<T> ListByProperty<T>(string propertyName, object value) where T : class;
        IList<T> ListByRange<T>(string propertyName, System.Collections.ICollection values) where T : class;
        IList<T> ListBySkipTake<T>(int startIndex, int take, string orderby = "Id", bool ascending = true) where T : class;
        IList<T> Like<T>(string propertyName, object value) where T : class;
        */
        #endregion
    }
}
