using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using HRMS.Models;
using HRMS.Common;
using System.Data;

namespace HRMS.DA
{
    public class DBAccess
    {
        public List<Role> GetListOfRoles()
        {
            try
            {
                // Fetching Data from DB
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(GlobalMembers.conString))
                {
                    SqlCommand command = new SqlCommand("Select RoleID,RoleName from Role", con);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    con.Open();
                    da.Fill(ds);
                    con.Close();
                }

                // Construcing return object
                List<Role> roles = new List<Role>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Role role = new Role()
                    {
                        RoleID = Convert.ToInt32(row["RoleID"].ToString()),
                        RoleName = row["RoleName"].ToString()
                    };
                    roles.Add(role);
                }
                return roles;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Role GetRole(int id)
        {
            try
            {
                // Fetching Data from DB
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(GlobalMembers.conString))
                {
                    SqlCommand command = new SqlCommand("GetRole", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RoleID", id);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    con.Open();
                    da.Fill(ds);
                    con.Close();
                }

                // Construcing return object
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Role role = new Role()
                    {
                        RoleID = Convert.ToInt32(ds.Tables[0].Rows[0]["RoleID"].ToString()),
                        RoleName = ds.Tables[0].Rows[0]["RoleName"].ToString()
                    };
                    return role;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Employee> GetListOfEmployees()
        {
            try
            {
                // Fetching Data from DB
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(GlobalMembers.conString))
                {
                    SqlCommand command = new SqlCommand("Select * from Employee", con);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    con.Open();
                    da.Fill(ds);
                    con.Close();
                }

                // Construcing return object
                List<Employee> employees = new List<Employee>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Employee employee = new Employee()
                    {
                        DateJoined = Convert.ToDateTime(row["DateJoined"].ToString()),
                        EmployeeID = Convert.ToInt32(row["EmployeeID"].ToString()),
                        EmployeeNumber = Convert.ToInt32(row["EmployeeNumber"].ToString()),
                        Extension = !String.IsNullOrEmpty(row["Extension"].ToString()) ? Convert.ToInt16(row["Extension"].ToString()) : 0,
                        FirstName = row["FirstName"].ToString(),
                        LastName = row["LastName"].ToString(),
                        Role = !String.IsNullOrEmpty(row["RoleID"].ToString()) ? GetRole(Convert.ToInt32(row["RoleID"].ToString())) : null
                    };
                    employees.Add(employee);
                }
                return employees;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteEmployee(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(GlobalMembers.conString))
                {
                    SqlCommand command = new SqlCommand("DeleteEmployee", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    command.ExecuteNonQuery();
                    con.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}