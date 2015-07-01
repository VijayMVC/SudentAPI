using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Query.Dynamic;
using Student.API.Models;
using Student.DependencyResolution;
using Student.Domain.Domain.Lookups;
using Student.Domain.Repositories;
using DomainStudent = Student.Domain.Domain.Sudents.Student;

namespace Student.API.Mappers.Students
{
    public static class StudentToStudentModel
    {
        public delegate void Transformer(DomainStudent input, StudentModel output);

        static readonly List<Transformer> Transformers = new List<Transformer>()
        {
            (d, m) => m.Id = d.Id,
            (d, m) => m.FirstName = d.FirstName,
            (d, m) => m.LastName = d.LastName,
            (d, m) => m.Major = d.Major.ShortDescription,
            (d, m) => m.Courses = d.StudentCourses.Select(StudentCourseToStudentCourseModel.Transform).ToList()
        };

        public static StudentModel Transform(DomainStudent input)
        {
            var output = new StudentModel();
            Transformers.ForEach(i => i(input, output));

            return output;
        }
    }
}