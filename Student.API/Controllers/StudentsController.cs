using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Student.Domain.Repositories;
using DomainStudent = Student.Domain.Domain.Sudents.Student;

namespace Student.API.Controllers
{
    public class StudentsController : ApiController
    {
        public IRepositoryProvider RepositoryProvider { get; set; }

        public StudentsController(IRepositoryProvider repositoryProvider)
        {
            RepositoryProvider = repositoryProvider;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                var student = RepositoryProvider.List<DomainStudent>();
                return Ok(student);
            }
            catch (Exception ex)
            {
                //TODO: Log Errors...

                return InternalServerError();
            }
        }
    }
}
