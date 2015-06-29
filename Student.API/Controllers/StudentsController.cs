using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json;
using Student.DependencyResolution;
using Student.Domain.Domain.Courses;
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


        /*
        NOTE: Json formatter is walking object tree and loading nhibernate collections
              Objects should be copied into a DTO for return and seperated from the Domain object and Session connection...
        */
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                var student = RepositoryProvider.List<DomainStudent>().ToList();
                return Ok(student);
            }
            catch (Exception ex)
            {
                //TODO: Log Errors...

                return InternalServerError();
            }
        }

        [HttpGet]
        public IHttpActionResult Get(Int32 id)
        {
            try
            {
                if (id == default(Int32))
                    return BadRequest();

                var student = RepositoryProvider.Get<DomainStudent>(id);

                return Ok(student);
            }
            catch (Exception ex)
            {
                //TODO: Log Errors...

                return InternalServerError();
            }
        }

        [HttpPost]
        public IHttpActionResult Post(DomainStudent student)
        {
            try
            {
                if (student == null || student.Id != 0)
                    return BadRequest();

                RepositoryProvider.Save(student);

                return Created(Request.RequestUri + "/" + student.Id, student);
            }
            catch (Exception ex)
            {
                //TODO: Log Errors...

                return InternalServerError();
            }
        }
    }
}
