using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiTest.Controllers
{
    [RoutePrefix("api/students")]
    public class StudentsController : ApiController
    {

        private SchoolContext _ctx;
        public SchoolContext Ctx
        {
            get
            {
                if (_ctx == null)
                {
                    _ctx = new SchoolContext();
                }
                return _ctx;
            }
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }


        [HttpGet]
        [Route("get_all")]
        public IHttpActionResult GetAll()
        {
            List<Student> students = Ctx.Students.ToList();
            return Ok(students);
        }

        [HttpGet]
        [Route("get_by_ncontrol/{nControl}")]
        public IHttpActionResult GetByNControl(int nControl)
        {
            Student student = Ctx.Students.SingleOrDefault(s => s.NControl == nControl);
            return Ok(student);
        }

        [HttpGet]
        [Route("get_by_name")]
        public IHttpActionResult GetAll(string search)
        {
            List<Student> students = Ctx.Students.Where(s => s.Name.Contains(search)).ToList();
            return Ok(students);
        }

        [HttpPost]
        [Route("insert")]
        public IHttpActionResult Insert(Student student)
        {
            if (student != null)
            {
                if (string.IsNullOrWhiteSpace(student.Name))
                {
                    return BadRequest("Student's Name cannot be empty.");
                }
                else
                {
                    Ctx.Students.Add(student);
                    Ctx.SaveChanges();
                    return Ok(true);
                }
            }
            else
            {
                return BadRequest("Student cannot be empty.");
            }
        }

        [HttpPut]
        [Route("edit")]
        public IHttpActionResult Edit(Student student)
        {
            if (student != null)
            {
                if (string.IsNullOrWhiteSpace(student.Name))
                {
                    return BadRequest("Student's Name cannot be empty.");
                }
                else
                {
                    Student tmpStudent = Ctx.Students.SingleOrDefault(s => s.NControl == student.NControl);
                    if (tmpStudent != null)
                    {
                        tmpStudent.Name = student.Name;
                        tmpStudent.Topic = student.Topic;
                        Ctx.SaveChanges();
                        return Ok(true);
                    }
                    else
                    {
                        return BadRequest(string.Format("Student with Control Number {0} does not exist.", student.NControl));
                    }
                }
            }
            else
            {
                return BadRequest("Student cannot be empty.");
            }
        }

        [HttpDelete]
        [Route("delete/{nControl}")]
        public IHttpActionResult Delete(int nControl)
        {
            Student student = Ctx.Students.FirstOrDefault(s => s.NControl == nControl);
            if (student != null)
            {
                Ctx.Students.Remove(student);
                Ctx.SaveChanges();
                return Ok(true);
            }
            else
            {
                return BadRequest(string.Format("Student with Control Number {0} does not exist.", nControl));
            }
        }
    }
}