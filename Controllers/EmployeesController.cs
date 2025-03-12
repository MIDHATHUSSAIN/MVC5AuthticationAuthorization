using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using webAPIandMVC.Models;
namespace webAPIandMVC.Controllers
{
    public class EmployeesController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                DataTable dtEmployees = new DataTable();
                string connectionString = ConfigurationManager.ConnectionStrings["graph"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_ALLEMPLOYEE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dtEmployees);
                        }
                    }
                }

                // Convert DataTable to a list of dictionaries (for JSON response)
                var employeeList = new List<Dictionary<string, object>>();
                foreach (DataRow row in dtEmployees.Rows)
                {
                    var employee = new Dictionary<string, object>();
                    foreach (DataColumn col in dtEmployees.Columns)
                    {
                        employee[col.ColumnName] = row[col];
                    }
                    employeeList.Add(employee);
                }

                return Ok(employeeList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] data employee)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["graph"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_INSERTEMPLOYEE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("NAME", employee.name);
                        cmd.Parameters.AddWithValue("email", employee.email);
                        cmd.Parameters.AddWithValue("Department", employee.Department);

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return Ok(new { success = true, message = "Employee added successfully!" });
                        }
                        else
                        {
                            return BadRequest("Failed to add employee.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]

        [Route("api/Employees/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["graph"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_DELETEBYIDEMPLOYEE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("id", id);

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return Ok(new { success = true, message = "Employee added successfully!" });
                        }
                        else
                        {
                            return BadRequest("Failed to add employee.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        //[Route("api/Employees/{id}")]
        public IHttpActionResult Put(int id, [FromBody] data empolyee)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["graph"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_UPDATEEMPLOYEE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("id", id);
                        cmd.Parameters.AddWithValue("NAME", empolyee.name);
                        cmd.Parameters.AddWithValue("email", empolyee.email);
                        cmd.Parameters.AddWithValue("Department", empolyee.Department);

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return Ok(new { success = true, message = "Employee added successfully!" });
                        }
                        else
                        {
                            return BadRequest("Failed to add employee.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        
    }
}

                                                                                                                                                                                                              