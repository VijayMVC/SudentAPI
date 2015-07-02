using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using Student.Domain.Repositories.Students;
using DomainStudent = Student.Domain.Domain.Sudents.Student;

namespace Student.DataAccess.Repositories.Students
{
    public class StudentRepository : RepositoryProvider, IStudentRepository
    {
        public List<DomainStudent> GetWithCourses()
        {
            var students = Session.CreateCriteria<DomainStudent>("S")
                .CreateCriteria("S.Major", "M", JoinType.LeftOuterJoin)
                .CreateCriteria("S.StudentCourses", "SC", JoinType.LeftOuterJoin)
                .CreateCriteria("SC.CourseInstance", "CI", JoinType.LeftOuterJoin)
                .SetResultTransformer(new DistinctRootEntityResultTransformer())
                .List<DomainStudent>().ToList();

            return students;
        }

        public DomainStudent GetWithCourses(Int32 studentId)
        {
            var student = Session.CreateCriteria<DomainStudent>("S")
                .Add(Restrictions.Eq(Projections.Property<DomainStudent>(s => s.Id), studentId))
                .CreateCriteria("S.Major", "M", JoinType.LeftOuterJoin)
                .CreateCriteria("S.StudentCourses", "SC", JoinType.LeftOuterJoin)
                .CreateCriteria("SC.CourseInstance", "CI", JoinType.LeftOuterJoin)
                .SetResultTransformer(new DistinctRootEntityResultTransformer())
                .List<DomainStudent>().FirstOrDefault();

            return student;
        }
    }
}
