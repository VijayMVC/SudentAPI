﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.Domain.Domain.Lookups
{
    public class Major : LookupBase
    {
        public virtual Department Department { get; set; }
    }
}
