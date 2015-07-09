using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Student.API.Helpers;
using Student.API.Mappers.Students;
using Student.Domain.Repositories;
using Student.Domain.Repositories.Students;

namespace Student.API.Controllers
{
    public class StudentCoursesController : BaseApiController
    {
        public IStudentCourseRepository StudentCourseRepository { get; set; }

        public StudentCoursesController(IStudentCourseRepository studentCourseRepository)
        {
            StudentCourseRepository = studentCourseRepository;
        }


        [HttpGet]
        [Route("api/Students/{studentId}/Courses")]
        public IHttpActionResult Get(Int32 studentId, String sort="Semester", String fields = null, Int32 page = 1, Int32 pageSize = 10)
        {
            try
            {
                var studentCourses = StudentCourseRepository.GetByStudentId(studentId);
                if (studentCourses == null)
                    return NotFound();

                var model = studentCourses
                    .Select(StudentCourseToStudentCourseModel.Transform)
                    .ApplySort(sort)
                    .ToList();

                var results = HandlePaging(model, sort, null, page, pageSize, MaxPageSize)
                    .ApplyFieldFiltering(fields);

                return Ok(results);
            }
            catch (Exception ex)
            {
                //TODO: Log Errors...

                return InternalServerError();
            }
        }
    }
}
