using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using webAPIandMVC.Models;
namespace webAPIandMVC.Controllers
{
    internal class EmployeeRepository
    {


        public List<data> GetAllEmployees()
        {
            var employees = new List<data>();
            string connectionString = ConfigurationManager.ConnectionStrings["graph"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_ALLEMPLOYEE", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(new data
                            {
                                id = Convert.ToInt32(reader["id"]),
                                name = reader["name"].ToString(),
                                email = reader["email"].ToString(),
                                Department = reader["Department"].ToString()
                            });
                        }
                    }
                }
            }
            return employees;
        }

        public bool AddEmployee(data employee)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["graph"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_INSERTEMPLOYEE", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NAME", employee.name);
                    cmd.Parameters.AddWithValue("@email", employee.email);
                    cmd.Parameters.AddWithValue("@Department", employee.Department);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }

        }

        public List<data> GetEmployeeById(int id)
        {
            var employees = new List<data>();
            string connectionString = ConfigurationManager.ConnectionStrings["graph"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GETBYIDEMPLOYEE", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(new data
                            {
                                id = Convert.ToInt32(reader["id"]),
                                name = reader["name"].ToString(),
                                email = reader["email"].ToString(),
                                Department = reader["Department"].ToString()
                            });
                        }
                    }
                }
            }
            return employees;
        }

        public bool UpdateEmployee(int id,data empolyee)
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
                            return true;
                        }
                        else
                        {
                            return false;
                        }            
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }


        }

        private bool InternalServerError(Exception ex)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEmployee(int id)
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
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public List<data> GetbyDP(string dp)
        {
            var employees = new List<data>();
            string connectionString = ConfigurationManager.ConnectionStrings["graph"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_FILTERED", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Department", dp);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(new data
                            {
                                id = Convert.ToInt32(reader["id"]),
                                name = reader["name"].ToString(),
                                email = reader["email"].ToString(),
                                Department = reader["Department"].ToString()
                            });
                        }
                    }
                }
            }
            return employees;
        }
    }
}