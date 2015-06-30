using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Student.API.Mappers.Students;
using Student.Domain.Repositories;
using Student.Domain.Repositories.Students;

namespace Student.API.Controllers
{
    public class StudentCoursesController : ApiController
    {
        public IRepositoryProvider RepositoryProvider { get; set; }
        public IStudentCourseRepository StudentCourseRepository { get; set; }

        public StudentCoursesController(IRepositoryProvider repositoryProvider, IStudentCourseRepository studentCourseRepository)
        {
            RepositoryProvider = repositoryProvider;
            StudentCourseRepository = studentCourseRepository;
        }


        [HttpGet]
        [Route("api/Students/{studentId}/Courses")]
        public IHttpActionResult Get(Int32 studentId)
        {
            try
            {
                var studentCourses = StudentCourseRepository.GetByStudentId(studentId);
                if (studentCourses == null)
                    return NotFound();

                var model = StudentCourseRepository.GetByStudentId(studentId)
                    .Select(StudentCourseToStudentCourseModel.Transform)
                    .ToList();

                return Ok(model);
            }
            catch (Exception ex)
            {
                //TODO: Log Errors...

                return InternalServerError();
            }
        }
    }
}
