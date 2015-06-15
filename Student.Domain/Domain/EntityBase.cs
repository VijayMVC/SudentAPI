using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.Domain.Domain
{
    public class EntityBase : AuditEntity
    {
        public virtual Int32 Id { get; set; }
    }
}
