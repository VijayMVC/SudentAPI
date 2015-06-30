using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Student.Domain.Domain.Courses;

namespace Student.Domain.Repositories.Courses
{
    public interface ICourseInstanceRepository : IRepositoryProvider
    {
        CourseInstance Find(String courseCode, String semesterDesc);
    }
}
