using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Student.API.Models;
using Student.DependencyResolution;
using Student.Domain.Domain.Lookups;
using Student.Domain.Repositories;
using DomainStudent = Student.Domain.Domain.Sudents.Student;

namespace Student.API.Mappers.Students
{
    public static class StudentModelToStudnet
    {
        public delegate void Transformer(StudentModel input, DomainStudent output, IRepositoryProvider rep);

        static readonly List<Transformer> Transformers = new List<Transformer>()
        {
            (d, m, r) => m.FirstName = d.FirstName,
            (d, m, r) => m.LastName = d.LastName,
            (d, m, r) => m.Major = r.FindSingleByProperty<Major>("ShortDescription", d.Major)
        };

        public static DomainStudent Transform(StudentModel input)
        {
            var rep = IocRegistration.IoCContainer.Resolve<IRepositoryProvider>();
            var output = rep.Get<DomainStudent>(input.Id) ?? new DomainStudent();
            Transformers.ForEach(i => i(input, output, rep));
            return output;
        }
    }
}