using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Student.Domain.Domain.Sudents;

namespace Student.Domain.Repositories.Students
{
    public interface IStudentCourseRepository : IRepositoryProvider
    {
        List<StudentCourse> GetByStudentId(Int32 studentId, Boolean eagerLoading = false);
        StudentCourse Find(Int32 studentId, String courseCode, String semesterDesc);
    }
}
