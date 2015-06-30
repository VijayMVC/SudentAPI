using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Criterion;
using Student.Domain.Domain.Courses;
using Student.Domain.Repositories.Courses;

namespace Student.DataAccess.Repositories.Courses
{
    public class CourseInstanceRepository : RepositoryProvider, ICourseInstanceRepository
    {
        public CourseInstance Find(string courseCode, string semesterDesc)
        {
            var courseInstance = Session.CreateCriteria<CourseInstance>("CI")
                .CreateCriteria("CI.Course", "C")
                .Add(Restrictions.Eq("C.CourseCode", courseCode))
                .CreateCriteria("CI.Semester", "S")
                .Add(Restrictions.Eq("S.ShortDescription", semesterDesc))
                .List<CourseInstance>().FirstOrDefault();

            return courseInstance;
        }
    }
}
