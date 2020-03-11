using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;
using EmployeeDataAccess;

namespace EmployeeService.Controllers
{
    /// <summary>
    /// Enable Cors for all methods in this controller
    /// </summary>
    [EnableCorsAttribute("*", "*", "*")]
    public class EmployeesController : ApiController
    {
        /// <summary>
        /// Cors is enabled for all methods in this controller 
        /// except this specific method
        /// </summary>
        /// 
        //[DisableCors]
        // GET api/<controller>
        //public IEnumerable<Employee> GetEmployee()
        //{
        //    using (EmployeeDBEntities entities = new EmployeeDBEntities())
        //    {
        //        return entities.Employees.ToList();
        //    }
        //}

        //[RequireHttps]
        [BasicAuthentication]
        public HttpResponseMessage Get(string gender="all")
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                switch (username.ToLower())
                {
                    //case "all":
                    //    return Request.CreateResponse(HttpStatusCode.OK, 
                    //        entities.Employees.ToList());
                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK, 
                            entities.Employees.Where(x => x.Gender.ToLower() == "male").ToList());
                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            entities.Employees.Where(x => x.Gender.ToLower() == "female").ToList());
                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                            $"Value for gender must be All, Male or Female. Provided code '{gender}' is invalid.");
                }
            }
        }

        // WEB API 1 has HttpResponseMessage
        // WEB API 2 introduces IHttpActionResult
        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult LoadEmployeeById(int id)
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                var entity = entities.Employees.FirstOrDefault(e => e.ID == id);

                if(entity != null)
                {
                    // FOR RETURN TYPE IHttpActionResult
                    return Ok(entity);

                    // FOR RETURN TYPE HttpResponseMessage
                    //return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    // FOR RETURN TYPE IHttpActionResult
                    return Content(HttpStatusCode.NotFound, $"Employee with id {id} not found.");

                    // FOR RETURN TYPE HttpResponseMessage
                    //return Request.CreateErrorResponse(HttpStatusCode.NotFound, 
                    //    $"Employee with id {id} not found.");
                }
            }
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]Employee employee)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    entities.Employees.Add(employee);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());

                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody]Employee employee)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(x => x.ID == id);

                    if (entity != null)
                    {
                        entity.FirstName = employee.FirstName;
                        entity.LastName = employee.LastName;
                        entity.Gender = employee.Gender;
                        entity.Salary = employee.Salary;

                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            $"Employee with ID {id} not found.");
                    }
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(x => x.ID == id);

                    if (entity != null)
                    {
                        entities.Employees.Remove(entity);
                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound,
                            $"Employee with Id = {id} not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}