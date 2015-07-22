using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainStudent = Student.Domain.Domain.Sudents.Student;

namespace Student.API.Models
{
    public class StudentModel
    {
        public Int32 Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Major { get; set; }
    }
}