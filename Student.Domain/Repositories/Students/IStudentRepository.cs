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
        List<DomainStudent> GetWithCourses();
        DomainStudent GetWithCourses(Int32 studentId);
    }
}
