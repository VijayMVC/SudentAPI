using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate;
using FluentNHibernate.Mapping;
using Student.Domain.Domain.Courses;
using Student.Domain.Domain.Sudents;

namespace Student.DataAccess.Maps.Students
{
    public sealed class StudentCourseMap : ClassMap<StudentCourse>
    {
        public StudentCourseMap()
        {
            Table("StudentCourse");
            Schema("dbo");
            CompositeId()
                .KeyReference(x => x.Student, "StudentId")
                .KeyReference(x => x.Course, "CourseId")
                .KeyReference(x => x.Semester, "SemesterId");

            Map(x => x.Grade, "Grade");
            Map(x => x.DateCreated);
            Map(x => x.DateModified);
            Map(x => x.UserCreated);
            Map(x => x.UserModified);

            References(x => x.CourseInstance)
                .Columns("CourseId", "SemesterId");

        }
    }
}
