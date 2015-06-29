using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using NHibernate.Persister.Entity;

namespace Student.DataAccess.Listeners
{
    public abstract class ListenerBase
    {
        protected const String DefaultUser = "System";

        protected string GetAuditUser()
        {
            var user = DefaultUser; // Should never be used but putting in for unknown cases

            if (Thread.CurrentPrincipal != null && !String.IsNullOrWhiteSpace(Thread.CurrentPrincipal.Identity.Name))
                user = Thread.CurrentPrincipal.Identity.Name;

            if (user == DefaultUser && HttpContext.Current != null && HttpContext.Current.User != null && !String.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
                user = HttpContext.Current.User.Identity.Name;

            if (user == DefaultUser && CallContext.LogicalGetData("UserName") != null)
                user = CallContext.LogicalGetData("UserName").ToString();

            return user;
        }

        protected void Set(IEntityPersister persister, IList<object> state, string propertyName, object value)
        {
            var index = Array.IndexOf(persister.PropertyNames, propertyName);
            if (index == -1)
                return;
            state[index] = value;
        }

        protected Boolean HasPropertyChanged(IEntityPersister persister, IList<object> oldState, IList<object> newState, string propertyName)
        {
            var index = Array.IndexOf(persister.PropertyNames, propertyName);
            if (index == -1)
                return false;

            return !String.Equals(oldState[index].ToString(), newState[index].ToString(), StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
