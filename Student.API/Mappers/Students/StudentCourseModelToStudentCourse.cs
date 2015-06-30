using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Web;
using Student.API.Models;
using Student.DependencyResolution;
using Student.Domain.Domain.Sudents;
using Student.Domain.Repositories;
using Student.Domain.Repositories.Courses;
using Student.Domain.Repositories.Students;
using DomainStudent = Student.Domain.Domain.Sudents.Student;

namespace Student.API.Mappers.Students
{
    public class StudentCourseModelToStudentCourse
    {
        public delegate void Transformer(StudentCourseModel input, StudentCourse output, ICourseInstanceRepository rep);

        static readonly List<Transformer> Transformers = new List<Transformer>()
        {
            (d, m, r) => m.Student = r.Get<DomainStudent>(d.StudentId),
            (d, m, r) => m.CourseInstance = r.Find(d.Course, d.Semester),
            (d, m, r) => m.Grade = d.Grade,
        };

        public static StudentCourse Transform(StudentCourseModel input, StudentCourse output = null)
        {
            var studentCourseRep = IocRegistration.IoCContainer.Resolve<IStudentCourseRepository>();
            var courseInstanceRep = IocRegistration.IoCContainer.Resolve<ICourseInstanceRepository>();

            if (output == null)
                output = studentCourseRep.Find(input.StudentId, input.Course, input.Semester) ?? new StudentCourse();
            Transformers.ForEach(i => i(input, output, courseInstanceRep));
            return output;
        }
    }
}