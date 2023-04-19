using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeService.Controllers
{
    public class EmployeesController : ApiController
    {
        public IEnumerable Get()
        {
            using (EmployeeDBContext db = new EmployeeDBContext())
            {
                return db.Employees.ToList();
            }
        }
        public Employee Get(int id)
        {
            using (EmployeeDBContext db = new EmployeeDBContext())
            {
                return db.Employees.FirstOrDefault(e => e.ID == id);
            }
        }

        // POST api/Users/CreateNew
        [HttpPost]
        public HttpResponseMessage CreateNew(Employee u)
        {
            try
            {
                using (EmployeeDBContext db = new EmployeeDBContext())
                {
                    db.Employees.Add(u);
                    db.SaveChanges();
                    var reponse = Request.CreateResponse(HttpStatusCode.Created, u);
                    reponse.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = u.ID }));
                    return reponse;
                }
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateEmployee(Employee u)
        {
            using (EmployeeDBContext db = new EmployeeDBContext())
            {
                var exist = db.Employees.Find(u.ID);
                if (exist != null)
                {
                    exist.FirstName = u.FirstName;
                    exist.LastName = u.LastName;
                    exist.Gender= u.Gender;
                    exist.Salary = u.Salary;
                    db.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
                return Ok(exist);
            }
            
        }

        [HttpDelete]
        public IHttpActionResult DeleteEmployee(int id)
        {
            using (EmployeeDBContext db = new EmployeeDBContext())
            {
                var exist = db.Employees.Find(id);
                if (exist != null)
                {
                    db.Employees.Remove(exist);
                    db.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
                return Ok(exist);
            }

        }
    }
}