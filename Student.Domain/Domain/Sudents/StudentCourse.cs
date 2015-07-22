using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Student.Domain.Domain.Courses;
using Student.Domain.Domain.Lookups;

namespace Student.Domain.Domain.Sudents
{
    public class StudentCourse : AuditEntity
    {
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
        public virtual Semester Semester { get; set; }

        public virtual CourseInstance CourseInstance { get; set; }
        public virtual Double Grade { get; set; }

        public override bool Equals(object obj)
        {
            var castedObject = obj as StudentCourse;
            if (castedObject == null)
                return false;

            if (castedObject.Student == this.Student && castedObject.Course == this.Course && castedObject.Semester == this.Semester)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return Student.GetHashCode() ^ Course.GetHashCode() ^ Semester.GetHashCode();
        }
    }
}
