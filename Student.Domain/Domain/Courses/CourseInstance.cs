using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Student.Domain.Domain.Lookups;

namespace Student.Domain.Domain.Courses
{
    public class CourseInstance : AuditEntity
    {
        private Int32 CPK_CourseId { get; set; }
        private Int32 CPK_SemesterId { get; set; }

        public virtual Int32 MaxStudentCount { get; set; }

        public virtual Course Course { get; set; }
        public virtual Semester Semester { get; set; }

        public virtual List<Sudents.Student> Students { get; set; }

        public override bool Equals(object obj)
        {
            var castedObject = obj as CourseInstance;
            if (castedObject == null)
                return false;

            if (castedObject.CPK_CourseId == this.CPK_CourseId && castedObject.CPK_SemesterId == this.CPK_SemesterId)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return CPK_CourseId.GetHashCode() ^ CPK_SemesterId.GetHashCode();
        }
    }
}
