using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Student.Domain.Domain.Lookups;

namespace Student.DataAccess.Maps.Lookups
{
    public sealed class DepartmentMap : ClassMap<Department>
    {
        public DepartmentMap()
        {
            Table("Department");
            Schema("dbo");
            Id(x => x.Id, "DepartmentId").GeneratedBy.Native();

            Map(x => x.ShortDescription, "DepartmentCode");
            Map(x => x.LongDescription, "DepartmentDesc");
            Map(x => x.DateCreated);
            Map(x => x.DateModified);
            Map(x => x.UserCreated);
            Map(x => x.UserModified);
        }
    }
}
