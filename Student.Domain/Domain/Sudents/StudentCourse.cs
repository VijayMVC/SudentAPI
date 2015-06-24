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
        private Int32 CPK_StudentId { get; set; }
        private Int32 CPK_CourseId { get; set; }
        private Int32 CPK_SemesterId { get; set; }

        public virtual Student Student { get; set; }
        public virtual CourseInstance CourseInstance { get; set; }
        public virtual Double Grade { get; set; }

        public override bool Equals(object obj)
        {
            var castedObject = obj as StudentCourse;
            if (castedObject == null)
                return false;

            if (castedObject.CPK_StudentId == this.CPK_StudentId && 
                castedObject.CPK_CourseId == this.CPK_CourseId &&
                castedObject.CPK_SemesterId == this.CPK_SemesterId)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return CPK_StudentId.GetHashCode() ^ CPK_CourseId.GetHashCode() ^ CPK_SemesterId.GetHashCode();
        }
    }
}
