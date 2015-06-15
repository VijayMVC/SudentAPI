using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
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
                .KeyProperty(x => x.Student, "StudentId")
                .KeyProperty(x => x.CourseInstance.Course, "CourseId")
                .KeyProperty(x => x.CourseInstance.Semester, "SemesterId");

            Map(x => x.Grade, "Grade");
            Map(x => x.DateCreated);
            Map(x => x.DateModified);
            Map(x => x.UserCreated);
            Map(x => x.UserModified);

            References(x => x.Student, "StudentId")
                .Cascade.None();
            References(x => x.CourseInstance, "CourseInstanceId")
                .Cascade.None();
        }
    }
}
