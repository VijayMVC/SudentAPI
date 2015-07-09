using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.Routing;
using Castle.Core.Internal;
using Newtonsoft.Json;
using Student.API.Mappers.Students;
using Student.API.Models;
using Student.DependencyResolution;
using Student.Domain.Domain.Courses;
using Student.Domain.Repositories;
using Student.API.Helpers;
using Student.Domain.Repositories.Students;
using DomainStudent = Student.Domain.Domain.Sudents.Student;

namespace Student.API.Controllers
{
    public class StudentsController : BaseApiController
    {
        public IStudentRepository StudentRepository { get; set; }

        public StudentsController(IStudentRepository studentRepository)
        {
            StudentRepository = studentRepository;
        }

        /*
        NOTE: NHibernateContractResolver in DataAccess is filtering out Lazy Loaded properties from result set.
        */
        [HttpGet]
        [Route("api/Students", Name = "StudentsList")]
        public IHttpActionResult Get(String sort = "Id", String fields = null, Int32 page = 1, Int32 pageSize = 10)
        {
            try
            {
                var students = StudentRepository.Get(eagerLoading: true)
                                .Select(StudentToStudentModel.Transform)
                                .ApplySort(sort)
                                .ToList();

                var results = HandlePaging(students, sort, fields, page, pageSize, MaxPageSize)
                    .ApplyFieldFiltering(fields);

                return Ok(results);
            }
            catch (Exception ex)
            {
                //TODO: Log Errors...

                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("api/Students/{id}")]
        public IHttpActionResult Get(Int32 id, String fields = null)
        {
            try
            {
                if (id == default(Int32))
                    return NotFound();

                var student = StudentRepository.Get(id, eagerLoading: true);
                var model = StudentToStudentModel.Transform(student)
                    .ApplyFieldFiltering(fields);

                return Ok(model);
            }
            catch (Exception ex)
            {
                //TODO: Log Errors...

                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("api/Students")]
        public IHttpActionResult Post([FromBody] StudentModel model)
        {
            //Insert New
            try
            {
                if (model == null || model.Id != 0)
                    return BadRequest();

                var student = StudentModelToStudent.Transform(model);
                StudentRepository.Save(student);

                return Created(Request.RequestUri + "/" + student.Id, student);
            }
            catch (Exception ex)
            {
                //TODO: Log Errors...

                return InternalServerError();
            }
        }

        [HttpPut]
        [Route("api/Students/{id}")]
        public IHttpActionResult Put(Int32 id, [FromBody] StudentModel model)
        {
            //Update
            try
            {
                if (id == 0 || model == null)
                    return BadRequest();

                model.Id = id;
                var student = StudentModelToStudent.Transform(model);

                if (student.Id == 0)
                {
                    StudentRepository.Evict(student);
                    return NotFound();
                }

                StudentRepository.Save(student);

                model = StudentToStudentModel.Transform(student);
                return Ok(model);
            }
            catch (Exception ex)
            {
                //TODO: Log Errors...

                return InternalServerError();
            }
        }

        #region Comments
        /// <remarks>
        /// Delta does not follow Patch specs.  It receives a partial object with the new values rather than
        ///     a list of actions to preform to update the document.  I chose to go with Delta because it's a
        ///     Microsoft library, not a 3rd party.
        /// </remarks>
        #endregion
        [HttpPatch]
        [Route("api/Students/{id}")]
        public IHttpActionResult Patch(Int32 id, [FromBody] Delta<StudentModel> delta)
        {
            #region Json Payload Example
            /*
            {
                "FirstName":"Bob",
                "LastName":"Smith"
            }
            */
            #endregion
            try
            {
                if (id == 0)
                    return BadRequest();

                var student = StudentRepository.Get(id);

                if (student == null)
                    return NotFound();

                var model = StudentToStudentModel.Transform(student);
                delta.Patch(model);

                student = StudentModelToStudent.Transform(model, student);

                StudentRepository.Save(student);

                return Ok(model);
            }
            catch (Exception ex)
            {
                //TODO: Log Errors...

                return InternalServerError();
            }
        }

        [HttpDelete]
        [Route("api/Students/{id}")]
        public IHttpActionResult Delete(Int32 id)
        {
            try
            {
                if (id == 0)
                    return BadRequest();

                var student = StudentRepository.Get(id);

                if (student == null)
                    return NotFound();

                StudentRepository.Delete(student);

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                //TODO: Log Errors...

                return InternalServerError();
            }
        }
    }
}
