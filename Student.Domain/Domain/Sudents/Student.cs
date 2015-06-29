using System;
using System.Collections.Generic;
using Student.Domain.Domain.Lookups;

namespace Student.Domain.Domain.Sudents
{
    public class Student : EntityBase
    {
        public virtual String FirstName { get; set; }
        public virtual String LastName { get; set; }
        public virtual Major Major { get; set; }

        public virtual IList<StudentCourse> StudentCourses { get; set; } 
    }
}
