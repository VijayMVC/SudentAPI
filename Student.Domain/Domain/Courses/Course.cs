using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Student.Domain.Domain.Lookups;

namespace Student.Domain.Domain.Courses
{
    public class Course : EntityBase
    {
        public virtual Department Department { get; set; }
        public virtual String Name { get; set; }
    }
}
