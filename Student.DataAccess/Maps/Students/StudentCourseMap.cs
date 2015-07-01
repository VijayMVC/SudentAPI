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
                .KeyProperty(Reveal.Member<StudentCourse>("CPK_StudentId"), "StudentId")
                .KeyProperty(Reveal.Member<StudentCourse>("CPK_CourseId"), "CourseId")
                .KeyProperty(Reveal.Member<StudentCourse>("CPK_SemesterId"), "SemesterId");

            Map(x => x.Grade, "Grade");
            Map(x => x.DateCreated);
            Map(x => x.DateModified);
            Map(x => x.UserCreated);
            Map(x => x.UserModified);

            References(x => x.Student, "StudentId")
                .Not.Insert()
                .Not.Update()
                .Cascade.None();

            References(x => x.CourseInstance)
                .Columns("CourseId", "SemesterId")
                .Not.Insert()
                .Not.Update()
                .Cascade.None();
        }
    }
}
