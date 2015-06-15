using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Web;
using NHibernate;
using NHibernate.Cfg.ConfigurationSchema;
using Student.Domain.Repositories;

namespace Student.DataAccess.Repositories
{
    public class RepositoryInit : IRepositoryInit
    {
        public string RepositoryKey { get; set; }
        public void InitDataContext(string connectionString)
        {
            InitDataContext(connectionString, null);
        }

        public void InitDataContext(string connectionString, List<Assembly> dataMappings)
        {
            if (String.IsNullOrWhiteSpace(RepositoryKey))
                RepositoryKey = connectionString;

            var config = new SessionProvider(dataMappings);
            config.Initialize(connectionString);
            NHibernateSessionProvider.AddSessionFactoryConfiguration(RepositoryKey, config);
        }

        public void BeginRequest(IsolationLevel isolation = IsolationLevel.ReadCommitted)
        {
            var config = NHibernateSessionProvider.SessionFactories[RepositoryKey];
            var session = config.SessionFactory.OpenSession();
            session.FlushMode = FlushMode.Never;
            var transaction = session.BeginTransaction(isolation);

            var context = CallContext.HostContext;
            if (context is HttpContext)
            {
                var http = (HttpContext)context;
                http.Items.Add(config.SessionKey, session);
                http.Items.Add(config.TransactionKey, transaction);
            }
            else
            {
                //CallContext.SetData was used at Sunflower but doesn't support multi-threading
                //CallContext.LogicalSetData will carry session to worker threads...
                //  TODO: need to see when end request is called (after main thread ends or after worker threads end)

                CallContext.LogicalSetData(config.SessionKey, session);
                CallContext.LogicalSetData(config.TransactionKey, transaction);
            }
        }

        public void EndRequest()
        {
            ISession session;
            ITransaction transaction;
            Exception exception = null;
            var config = NHibernateSessionProvider.SessionFactories[RepositoryKey];

            var context = CallContext.HostContext;
            if (context is HttpContext)
            {
                transaction = ((HttpContext)context).Items[config.TransactionKey] as ITransaction;
                session = ((HttpContext)context).Items[config.SessionKey] as ISession;
            }
            else
            {
                transaction = CallContext.LogicalGetData(config.TransactionKey) as ITransaction;
                session = CallContext.LogicalGetData(config.SessionKey) as ISession;
            }

            try
            {
                if (transaction != null)
                    transaction.Commit();
            }
            catch (Exception ex)
            {
                exception = ex;
                if (transaction != null)
                    transaction.Rollback();
            }
            finally
            {
                if (transaction != null)
                    transaction.Dispose();
            }

            if (session != null)
            {
                session.Close();
                session.Dispose();
            }

            if (exception != null)
                throw exception;
        }

        public void Dispose()
        {
            foreach (var kvp in NHibernateSessionProvider.SessionFactories)
            {
                kvp.Value.SessionFactory.Close();
                kvp.Value.SessionFactory.Dispose();
            }
        }
    }
}
