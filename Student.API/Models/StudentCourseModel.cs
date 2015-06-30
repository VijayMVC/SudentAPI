using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Student.API.Models
{
    public class StudentCourseModel
    {
        public Int32 StudentId { get; set; }
        public String Course { get; set; }
        public String Semester { get; set; }
        public Double Grade { get; set; }
    }
}