using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NHibernate;
using NHibernate.Criterion;
using Student.Domain.Repositories;

namespace Student.DataAccess.Repositories
{
    public class RepositoryProvider : IRepositoryProvider
    {
        public string RepositoryKey { get; set; }

        public ISession Session
        {
            get
            {
                if (String.IsNullOrWhiteSpace(RepositoryKey))
                    Debug.WriteLine(RepositoryKey);

                ISession session = null;
                if (CallContext.HostContext is HttpContext)
                    session = ((HttpContext)CallContext.HostContext).Items[NHibernateSessionProvider.SessionFactories[RepositoryKey].SessionKey] as ISession;
                else
                    session = CallContext.LogicalGetData(NHibernateSessionProvider.SessionFactories[RepositoryKey].SessionKey) as ISession;

                Debug.WriteLine(String.Format("Repository Provider, ThreadId: {0}, Ticks: {1}", System.Threading.Thread.CurrentThread.ManagedThreadId, DateTime.Now.Ticks));
                if (session == null)
                {
                    Debug.WriteLine("Session is null. " + this.GetType());
                }
                return session;
            }
        }

        public T Get<T>(object id)
        {
            return Session.Get<T>(id);
        }

        public T Load<T>(object id)
        {
            return Session.Load<T>(id);
        }

        public void Evict<T>(T item)
        {
            Session.Evict(item);
        }

        public void Save<T>(T item)
        {
            Session.SaveOrUpdate(item);
            Session.Flush();
        }

        public void Delete<T>(T item)
        {
            Session.Delete(item);
            Session.Flush();
        }

        public IList<T> List<T>()
        {
            var crit = Session.CreateCriteria(typeof(T));
            return crit.List<T>();
        }

        public IList<T> ListByProperty<T>(string propertyName, object value) where T : class
        {
            return Session.CreateCriteria<T>().Add(Restrictions.Eq(propertyName, value)).List<T>();
        }

        public IList<T> ListByRange<T>(string propertyName, System.Collections.ICollection values) where T : class
        {
            return Session.CreateCriteria<T>().Add(Restrictions.In(propertyName, values)).List<T>();
        }

        public IList<T> ListBySkipTake<T>(int startIndex, int take, string orderby = "Id", bool ascending = true) where T : class
        {
            return Session.QueryOver<T>().Skip(startIndex).Take(take).RootCriteria.AddOrder(ascending ? Order.Asc(orderby) : Order.Desc(orderby)).List<T>();
        }

        public IList<T> Like<T>(string propertyName, object value) where T : class
        {
            return Session.CreateCriteria<T>().Add(Restrictions.InsensitiveLike(propertyName, "%" + value.ToString() + "%")).List<T>();
        }

        public T FindSingleByProperty<T>(string propertyName, object value) where T : class
        {
            var result = Session.CreateCriteria<T>().Add(Restrictions.Eq(propertyName, value)).List<T>();
            return result.Any() ? result.First() : null;
        }

        public void ForceFlush()
        {
            Session.Flush();
        }

        public IDbCommand GetCommand(bool enlistTransaction)
        {
            var cmd = Session.Connection.CreateCommand();

            if (enlistTransaction)
            {
                if ((Session.Transaction == null) || (!Session.Transaction.IsActive) || (Session.Transaction.WasCommitted) || (Session.Transaction.WasRolledBack))
                    Session.BeginTransaction(IsolationLevel.ReadCommitted);
                Session.Transaction.Enlist(cmd);
            }

            return cmd;
        }
    }
}
