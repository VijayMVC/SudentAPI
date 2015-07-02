using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Student.API.Models;
using Student.DependencyResolution;
using Student.Domain.Domain.Lookups;
using Student.Domain.Domain.Sudents;
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
            (d, m, r) => MapStudentCourses(m, d)
        };

        public static DomainStudent Transform(StudentModel input, DomainStudent output = null)
        {
            var rep = IocRegistration.IoCContainer.Resolve<IStudentRepository>();
            if(output == null)
                output = rep.GetWithCourses(input.Id) ?? new DomainStudent();
            Transformers.ForEach(i => i(input, output, rep));
            return output;
        }

        private static void MapStudentCourses(DomainStudent student, StudentModel model)
        {
            var modelCourses = model.Courses.Select(c => StudentCourseModelToStudentCourse.Transform(c)).ToList();
            var studentCourses = student.StudentCourses.ToList();

            var coursesToAdd = modelCourses.Except(studentCourses).ToList();
            var coursesToRemove = studentCourses.Except(modelCourses).ToList();

            foreach (var courseToRemove in coursesToRemove)
                student.StudentCourses.Remove(courseToRemove);

            foreach(var courseToAdd in coursesToAdd)
                student.StudentCourses.Add(courseToAdd);
        }
    }
}