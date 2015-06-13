using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.Domain.Domain.Lookups
{
    public class LookupBase : EntityBase
    {
        public virtual String ShortDescription { get; set; }
        public virtual String LongDescription { get; set; }

        public override string ToString()
        {
            return ShortDescription;
        }
    }
}
