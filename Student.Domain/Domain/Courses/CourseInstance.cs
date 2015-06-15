using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Student.Domain.Domain.Lookups;

namespace Student.Domain.Domain.Courses
{
    public class CourseInstance : EntityBase
    {
        public virtual Int32 MaxStudentCount { get; set; }

        public virtual Course Course { get; set; }
        public virtual Semester Semester { get; set; }

        public virtual List<Sudents.Student> Students { get; set; } 
    }
}
