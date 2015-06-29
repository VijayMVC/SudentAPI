using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Event;
using Student.Domain.Domain;

namespace Student.DataAccess.Listeners
{
    public class Update : ListenerBase, IPreUpdateEventListener
    {
        public bool OnPreUpdate(PreUpdateEvent @event)
        {
            if (!(@event.Entity is AuditEntity))
                return false;
            var entity = @event.Entity as AuditEntity;

            var time = DateTime.Now;
            var userName = GetAuditUser();

            Set(@event.Persister, @event.State, "DateModified", time);
            entity.DateModified = time;
            Set(@event.Persister, @event.State, "UserModified", userName);
            entity.UserModified = userName;

            return false;
        }
    }
}
