using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.Domain.Domain
{
    public class EntityBase
    {
        public virtual Int32 Id { get; set; }

        public virtual String UserCreated { get; set; }
        public virtual String UserModified { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual DateTime DateModified { get; set; }
    }
}
