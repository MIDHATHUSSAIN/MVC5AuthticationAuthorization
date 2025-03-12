using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using webAPIandMVC.Models;

namespace webAPIandMVC.Controllers
{
      [JwtAuthentication]
    public class Employee2Controller : ApiController
    {

        private readonly EmployeeRepository _repository = new EmployeeRepository();

        [HttpGet]
        public IHttpActionResult Get()
        {
            var role = Request.Properties.ContainsKey("UserRole") ? Request.Properties["UserRole"].ToString() : null;

            // Authorization check
            if (role != "abcccccc@gmail.com")
            {
                return Unauthorized(); 
            }



            var employees = _repository.GetAllEmployees();
            
            return Ok(employees);
        }

        
        [HttpPost]
        public IHttpActionResult Post([FromBody] data employee)
        {
            if (_repository.AddEmployee(employee))
                return Ok(new { success = true, message = "Employee added successfully!" });

            return BadRequest("Failed to add employee.");
        }

        [HttpGet]
        [Route("api/Employee2/{id}")]
        public IHttpActionResult Get(int id)
        {
            var employee = _repository.GetEmployeeById(id);
            return Ok(employee);
        }

        [HttpPut]
        [Route("api/Employee2/{id}")]
        public IHttpActionResult Put(int id, [FromBody] data employee)
        {
            if (_repository.UpdateEmployee(id, employee))
                return Ok(new { success = true, message = "Employee updated successfully!" });

            return BadRequest("Failed to update employee.");
        }

        [HttpDelete]
        [Route("api/Employee2/{id}")]
        public IHttpActionResult Delete(int id)
        {
            if (_repository.DeleteEmployee(id))
                return Ok(new { success = true, message = "Employee deleted successfully!" });

            return BadRequest("Failed to delete employee.");
        }

        [HttpGet]

        public IHttpActionResult Get(string dp)
        {
            var employeebydp = _repository.GetbyDP(dp);
            return Ok(employeebydp);
        }
    }
}
