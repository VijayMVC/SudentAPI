using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode.Impl;

using DomainStudent = Student.Domain.Domain.Sudents.Student;

namespace Student.DataAccess.Maps.Students
{
    public sealed class StudentMap : ClassMap<DomainStudent>
    {
        public StudentMap()
        {
            Table("Student");
            Schema("dbo");
            Id(x => x.Id, "StudentId").GeneratedBy.Native();

            Map(x => x.FirstName, "FirstName");
            Map(x => x.LastName, "LastName");
            Map(x => x.DateCreated);
            Map(x => x.DateModified);
            Map(x => x.UserCreated);
            Map(x => x.UserModified);

            References(x => x.Major, "MajorId")
                .Cascade.None();

            //HasMany(x => x.StudentCourses)
            //    .KeyColumn("StudentId")
            //    //.Inverse()
            //    .Cascade.All();
        }
    }
}
