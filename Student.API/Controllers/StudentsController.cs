using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using Castle.Core.Internal;
using Newtonsoft.Json;
using Student.API.Mappers.Students;
using Student.API.Models;
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
        NOTE: NHibernateContractResolver in DataAccess is filtering out Lazy Loaded properties from result set.
        */
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                var student = RepositoryProvider
                                .List<DomainStudent>()
                                .Select(StudentToStudentModel.Transform)
                                .ToList();
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
                    return NotFound();

                var student = RepositoryProvider.Get<DomainStudent>(id);
                var model = StudentToStudentModel.Transform(student);

                return Ok(model);
            }
            catch (Exception ex)
            {
                //TODO: Log Errors...

                return InternalServerError();
            }
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] StudentModel model)
        {
            //Insert New
            try
            {
                if (model == null || model.Id != 0)
                    return BadRequest();

                var student = StudentModelToStudnet.Transform(model);
                RepositoryProvider.Save(student);

                return Created(Request.RequestUri + "/" + student.Id, student);
            }
            catch (Exception ex)
            {
                //TODO: Log Errors...

                return InternalServerError();
            }
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody] StudentModel model)
        {
            //Update
            try
            {
                if (model == null || model.Id == 0)
                    return BadRequest();

                var student = StudentModelToStudnet.Transform(model);

                if (student.Id == 0)
                {
                    RepositoryProvider.Evict(student);
                    return NotFound();
                }

                RepositoryProvider.Save(student);

                model = StudentToStudentModel.Transform(student);
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
