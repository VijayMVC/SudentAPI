using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Student.Domain.Domain.Courses;
using Student.Domain.Domain.Lookups;

namespace Student.Domain.Domain.Sudents
{
    public class StudentCourse : EntityBase
    {
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
        public virtual Double Grade { get; set; }
        public virtual Semester Semester { get; set; }
    }
}
