using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Student.Domain.Domain.Lookups;

namespace Student.DataAccess.Maps.Lookups
{
    public sealed class MajorMap : ClassMap<Major>
    {
        public MajorMap()
        {
            Table("Major");
            Schema("dbo");
            Id(x => x.Id, "MajorId").GeneratedBy.Native();

            Map(x => x.ShortDescription, "MajorCode");
            Map(x => x.LongDescription, "MajorDesc");
            Map(x => x.DateCreated);
            Map(x => x.DateModified);
            Map(x => x.UserCreated);
            Map(x => x.UserModified);

            References(x => x.Department, "DepartmentId")
                .Cascade.None();
        }
    }
}
