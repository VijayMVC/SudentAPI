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
        public virtual Major Major { get; set; }
        public virtual String CourseCode { get; set; }
        public virtual String CourseDesc { get; set; }
    }
}
