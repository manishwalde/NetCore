using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EmployeeService.Controllers
{
    [EnableCorsAttribute("*","*","*")]
    //// Registered it in WebApiConfig file
    //[RequireHttps]
    public class ValuesController : ApiController
    {
        static List<string> svalues = new List<string>()
        {
            "value0", "value1", "value2", "value4", "value5"
        };
        // GET api/values
        [DisableCors]
        public IEnumerable<string> Get()
        {
            return svalues;
        }

        /*
        // GET api/values/All
        // GET api/values?gender=all
        [RequireHttps]
        [BasicAuthentication]
        public HttpResponseMessage Get(string gender = "All")
        {
            string username = Thread.CurrentPrincipal.Identity.Name;

            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                switch (username.ToLower()) // (gender.ToLower())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            entities.Employees.ToList());
                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            entities.Employees.Where(e => e.Gender.ToLower() == "male").ToList());
                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            entities.Employees.Where(e => e.Gender.ToLower() == "female").ToList());
                    default:                        
                        return Request.CreateResponse(HttpStatusCode.BadRequest);
                        // return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        //    $"Value for gender must be Male, Female or All. {gender} is invalid.");
                }
            }
        }
        */

        /*
         * 1. If a method return type is void, 
         * by default status code 204 No Content is returned.
         * 2. When an item is not found, instead of returning NULL and status code 200 OK, 
         * return 404 Not Found status code along with a meaningful message such as "Employee with Id = 101 not found"
         */
        // GET api/values/5
        [HttpGet]
        public HttpResponseMessage LoadById(int id)
        {
            try
            {
                if (svalues.Count > id)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, svalues[id]);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        $"Item with Id {id.ToString()} not found");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

            /*
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        $"Employee with Id {id.ToString()} not found");
                }
            }
            */
        }

        /*
         * 1. When a new item is created, 
         * we should be returning status code 201 Item Created.
         * 2. With 201 status code we should also include the location 
         * i.e URI of the newly created item. 
         */
        // POST api/values
        public HttpResponseMessage Post([FromBody]string value)
        {
            svalues.Add(value);
            var message = Request.CreateResponse(HttpStatusCode.Created, value);
            return message;

            /*
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    entities.Employees.Add(employee);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri +
                        employee.ID.ToString());

                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            */
        }

        // PUT api/values/5
        public HttpResponseMessage Put([FromUri]int id, [FromBody]string value)
        {
            try
            {
                if (svalues.Count > id)
                {
                    svalues[id] = value;
                    return Request.CreateResponse(HttpStatusCode.OK, value);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                                $"Item with Id { id.ToString() } not found to update");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

            /*
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Employee with Id " + id.ToString() + " not found to update");
                    }
                    else
                    {
                        entity.FirstName = employee.FirstName;
                        entity.LastName = employee.LastName;
                        entity.Gender = employee.Gender;
                        entity.Salary = employee.Salary;

                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            */
        }

        // DELETE api/values/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                if (svalues.Count > id)
                {
                    svalues.RemoveAt(id);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                                $"Item with Id = {id.ToString()} not found to delete");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

            /*
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Employee with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.Employees.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            */
        }
    }
}
