using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using Student.Domain.Domain.Courses;
using Student.Domain.Domain.Lookups;
using Student.Domain.Domain.Sudents;
using Student.Domain.Repositories.Students;

namespace Student.DataAccess.Repositories.Students
{
    public class StudentCourseRepository : RepositoryProvider, IStudentCourseRepository
    {
        public List<StudentCourse> GetByStudentId(Int32 studentId, Boolean eagerLoading = false)
        {
            var criteria = Session.CreateCriteria<StudentCourse>("SC")
                .Add(Restrictions.Eq(Projections.Property<StudentCourse>(sc => sc.Student.Id), studentId));

            if (eagerLoading)
            {
                criteria.CreateCriteria("SC.CourseInstance", "CI", JoinType.LeftOuterJoin)
                    .CreateCriteria("CI.Course", "C", JoinType.LeftOuterJoin)
                    .CreateCriteria("CI.Semester", "S", JoinType.LeftOuterJoin);
            }

            var studentCourses = criteria.SetResultTransformer(new DistinctRootEntityResultTransformer())
                .List<StudentCourse>().ToList();

            return studentCourses;
        }

        public StudentCourse Find(Int32 studentId, String courseCode, String semesterDesc)
        {
            var courseInstance = Session.CreateCriteria<CourseInstance>("CI")
                .CreateCriteria("CI.Course", "C")
                .Add(Restrictions.Eq("C.CourseCode", courseCode))
                .CreateCriteria("CI.Semester", "S")
                .Add(Restrictions.Eq("S.ShortDescription", semesterDesc))
                .SetResultTransformer(new DistinctRootEntityResultTransformer())
                .List<CourseInstance>().FirstOrDefault();

            if (courseInstance == null)
                return null;

            var studentCourse = Session.CreateCriteria<StudentCourse>("SC")
                .Add(Restrictions.Eq(Projections.Property<StudentCourse>(sc => sc.Student.Id), studentId))
                .Add(Restrictions.Eq(Projections.Property<StudentCourse>(sc => sc.CourseInstance.Course.Id), courseInstance.Course.Id))
                .Add(Restrictions.Eq(Projections.Property<StudentCourse>(sc => sc.CourseInstance.Semester.Id), courseInstance.Semester.Id))
                .SetResultTransformer(new DistinctRootEntityResultTransformer())
                .List<StudentCourse>().FirstOrDefault();

            return studentCourse;
        }
    }
}
