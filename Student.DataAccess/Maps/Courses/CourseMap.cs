using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Student.Domain.Domain.Courses;

namespace Student.DataAccess.Maps.Courses
{
    public sealed class CourseMap : ClassMap<Course>
    {
        public CourseMap()
        {
            Table("Course");
            Schema("dbo");
            Id(x => x.Id, "CourseId").GeneratedBy.Native();

            Map(x => x.Name, "Name");
            Map(x => x.DateCreated);
            Map(x => x.DateModified);
            Map(x => x.UserCreated);
            Map(x => x.UserModified);

            References(x => x.Department, "DepartmentId")
                .Cascade.None();
        }
    }
}
