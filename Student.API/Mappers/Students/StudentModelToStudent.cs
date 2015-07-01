using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Student.API.Models;
using Student.DependencyResolution;
using Student.Domain.Domain.Lookups;
using Student.Domain.Repositories;
using Student.Domain.Repositories.Students;
using DomainStudent = Student.Domain.Domain.Sudents.Student;

namespace Student.API.Mappers.Students
{
    public static class StudentModelToStudent
    {
        public delegate void Transformer(StudentModel input, DomainStudent output, IRepositoryProvider rep);

        static readonly List<Transformer> Transformers = new List<Transformer>()
        {
            (d, m, r) => m.FirstName = d.FirstName,
            (d, m, r) => m.LastName = d.LastName,
            (d, m, r) => m.Major = r.FindSingleByProperty<Major>("ShortDescription", d.Major),
            (d, m, r) => m.StudentCourses = d.Courses.Select(c => StudentCourseModelToStudentCourse.Transform(c)).ToList()
        };

        public static DomainStudent Transform(StudentModel input, DomainStudent output = null)
        {
            var rep = IocRegistration.IoCContainer.Resolve<IStudentRepository>();
            if(output == null)
                output = rep.GetWithCourses(input.Id) ?? new DomainStudent();
            Transformers.ForEach(i => i(input, output, rep));
            return output;
        }
    }
}