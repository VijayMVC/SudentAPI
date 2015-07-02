using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate;
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
                .KeyReference(x => x.Course, "CourseId")
                .KeyReference(x => x.Semester, "SemesterId");

            Map(x => x.MaxStudentCount, "MaxStudentCount");
            Map(x => x.DateCreated);
            Map(x => x.DateModified);
            Map(x => x.UserCreated);
            Map(x => x.UserModified);

            HasManyToMany(x => x.Students)
                .Table("StudentCourse")
                .ChildKeyColumn("StudentId")
                .ParentKeyColumns.Add("CourseId", "SemesterId")
                .Inverse()
                .Cascade.None();
        }
    }
}
