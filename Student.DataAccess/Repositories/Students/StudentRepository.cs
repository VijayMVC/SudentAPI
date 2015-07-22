using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using Student.Domain.Repositories.Students;
using DomainStudent = Student.Domain.Domain.Sudents.Student;

namespace Student.DataAccess.Repositories.Students
{
    public class StudentRepository : RepositoryProvider, IStudentRepository
    {
        #region Create/Update

        public void Insert(DomainStudent student)
        {
            Session.Save(student);

            if (student.StudentCourses != null)
            {
                foreach (var course in student.StudentCourses)
                {
                    Session.Save(course);
                }
            }

            Session.Flush();
        }

        public void Update(DomainStudent student)
        {
            Session.Update(student);
            Session.Flush();
        }

        public void Evict(DomainStudent student)
        {
            Session.Evict(student);
        }

        #endregion

        #region Read
        public List<DomainStudent> Get(Boolean eagerLoading = false)
        {
            var studentsCriteria = Session.CreateCriteria<DomainStudent>("S");

            if (eagerLoading)
            {
                studentsCriteria.CreateCriteria("S.Major", "M", JoinType.LeftOuterJoin)
                    .CreateCriteria("S.StudentCourses", "SC", JoinType.LeftOuterJoin)
                    .CreateCriteria("SC.CourseInstance", "CI", JoinType.LeftOuterJoin);
            }

            var students = studentsCriteria.SetResultTransformer(new DistinctRootEntityResultTransformer())
                .List<DomainStudent>().ToList();

            return students;
        }

        public DomainStudent Get(Int32 studentId, Boolean eagerLoading = false)
        {
            var studentCriteria = Session.CreateCriteria<DomainStudent>("S")
                .Add(Restrictions.Eq(Projections.Property<DomainStudent>(s => s.Id), studentId));

            if (eagerLoading)
            {
                studentCriteria.CreateCriteria("S.Major", "M", JoinType.LeftOuterJoin)
                    .CreateCriteria("S.StudentCourses", "SC", JoinType.LeftOuterJoin)
                    .CreateCriteria("SC.CourseInstance", "CI", JoinType.LeftOuterJoin);
            }

            var student = studentCriteria.SetResultTransformer(new DistinctRootEntityResultTransformer())
                .List<DomainStudent>().FirstOrDefault();

            return student;
        }
        #endregion

        #region Delete

        public void Delete(DomainStudent student)
        {
            Session.Delete(student);
            Session.Flush();
        }
        #endregion
    }
}
