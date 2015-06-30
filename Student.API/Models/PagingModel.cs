using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Student.API.Models
{
    public class PagingModel
    {
        public static Int32 MaxPageSize = 10;

        public String Sort { get; set; }
        public Int32 Page { get; set; }
        public Int32 PageSize { get; set; }

        public PagingModel()
        {
            Sort = "Id";
            Page = 1;
            PageSize = 10;
        }
    }
}