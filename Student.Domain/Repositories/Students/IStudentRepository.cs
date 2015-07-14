using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainStudent = Student.Domain.Domain.Sudents.Student;

namespace Student.Domain.Repositories.Students
{
    public interface IStudentRepository : IRepositoryProvider
    {
        void Insert(DomainStudent student);
        void Update(DomainStudent student);
        void Evict(DomainStudent student);

        List<DomainStudent> Get(Boolean eagerLoading = false);
        DomainStudent Get(Int32 studentId, Boolean eagerLoading = false);

        void Delete(DomainStudent student);
    }
}
