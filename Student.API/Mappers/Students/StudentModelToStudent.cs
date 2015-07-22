using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Student.API.Models;
using Student.DependencyResolution;
using Student.Domain.Domain.Lookups;
using Student.Domain.Domain.Sudents;
using Student.Domain.Repositories;
using Student.Domain.Repositories.Lookups;
using Student.Domain.Repositories.Students;
using DomainStudent = Student.Domain.Domain.Sudents.Student;

namespace Student.API.Mappers.Students
{
    public static class StudentModelToStudent
    {
        public delegate void Transformer(StudentModel input, DomainStudent output, ILookupRepository rep);

        static readonly List<Transformer> Transformers = new List<Transformer>()
        {
            (d, m, r) => m.FirstName = d.FirstName,
            (d, m, r) => m.LastName = d.LastName,
            (d, m, r) => m.Major = r.Find<Major>(d.Major)
        };

        public static DomainStudent Transform(StudentModel input, DomainStudent output = null)
        {
            var studentRepository = IocRegistration.IoCContainer.Resolve<IStudentRepository>();
            var lookupRepository = IocRegistration.IoCContainer.Resolve<ILookupRepository>();

            if (output == null)
                output = studentRepository.Get(input.Id, eagerLoading: true);

            if (output == null)
                output = new DomainStudent();

            Transformers.ForEach(i => i(input, output, lookupRepository));
            return output;
        }
    }
}