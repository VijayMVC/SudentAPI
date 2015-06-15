using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Student.Domain.Domain.Lookups;

namespace Student.DataAccess.Maps.Lookups
{
    public sealed class SemesterMap : ClassMap<Semester>
    {
        public SemesterMap()
        {
            Table("Semester");
            Schema("dbo");
            Id(x => x.Id, "SemesterId").GeneratedBy.Native();

            Map(x => x.ShortDescription, "SemesterCode");
            Map(x => x.LongDescription, "SemesterDesc");
            Map(x => x.DateCreated);
            Map(x => x.DateModified);
            Map(x => x.UserCreated);
            Map(x => x.UserModified);
        }
    }
}
