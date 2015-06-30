using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Student.API.Models;
using Student.Domain.Domain.Sudents;
using DomainStudent = Student.Domain.Domain.Sudents.Student;

namespace Student.API.Mappers.Students
{
    public class StudentCourseToStudentCourseModel
    {
        public delegate void Transformer(StudentCourse input, StudentCourseModel output);

        static readonly List<Transformer> Transformers = new List<Transformer>()
        {
            (d, m) => m.StudentId = d.Student.Id,
            (d, m) => m.Course = d.CourseInstance.Course.CourseCode,
            (d, m) => m.Semester = d.CourseInstance.Semester.ShortDescription,
            (d, m) => m.Grade = d.Grade
        };

        public static StudentCourseModel Transform(StudentCourse input)
        {
            var output = new StudentCourseModel();
            Transformers.ForEach(i => i(input, output));
            return output;
        }
    }
}