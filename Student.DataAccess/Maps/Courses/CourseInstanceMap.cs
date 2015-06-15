using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Student.Domain.Domain.Courses;

namespace Student.DataAccess.Maps.Courses
{
    public sealed class CourseInstanceMap : ClassMap<CourseInstance>
    {
        public CourseInstanceMap()
        {
            Table("CourseInstance");
            Schema("dbo");
            CompositeId()
                .KeyProperty(x => x.Course, "CourseId")
                .KeyProperty(x => x.Semester, "SemesterId");

            Map(x => x.MaxStudentCount, "MaxStudentCount");
            Map(x => x.DateCreated);
            Map(x => x.DateModified);
            Map(x => x.UserCreated);
            Map(x => x.UserModified);

            References(x => x.Course, "CourseId")
                .Cascade.None();
            References(x => x.Semester, "SemesterId")
                .Cascade.None();

            //HasManyToMany(x => x.Students)
            //    .Table("StudentCourse")
            //    .ParentKeyColumn("StudentId")
            //    .ChildKeyColumn("CourseInstanceId")
            //    .Inverse()
            //    .Cascade.None();
        }
    }
}
