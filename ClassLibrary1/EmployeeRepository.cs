using EmployeeConsoleADO.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using EmployeeConsoleADO.Data.Interfaces;
namespace EmployeeConsoleADO.Data; 
public class EmployeeRepository : IEmployeeRepository
{
    private readonly string connectionString;

    public EmployeeRepository(string _ConnectionString)
    {
        connectionString = _ConnectionString;
    }

    public void AddEmployee(Employee employee)
    {
        using(var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string sql = @"INSERT INTO Employee ( FirstName, LastName, DateOfBirth, Email, Mobile, JoiningDate, RoleId, Manager, Project)
                               VALUES ( @FirstName, @LastName, @DateOfBirth, @Email, @Mobile, @JoiningDate, @RoleId, @Manager, @Project)";

            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@LastName", (object)employee.LastName ?? DBNull.Value);
                command.Parameters.AddWithValue("@DateOfBirth", (object)employee.DateOfBirth ?? DBNull.Value);
                command.Parameters.AddWithValue("@Email", employee.Email);
                command.Parameters.AddWithValue("@Mobile", (object)employee.Mobile ?? DBNull.Value);
                command.Parameters.AddWithValue("@JoiningDate", employee.JoiningDate);
                command.Parameters.AddWithValue("@RoleId", employee.RoleId);
                command.Parameters.AddWithValue("@Manager", (object)employee.Manager ?? DBNull.Value);
                command.Parameters.AddWithValue("@Project", (object)employee.Project ?? DBNull.Value);
                command.ExecuteNonQuery();
            }
        }
    }
    public List<Employee> GetAllEmployees()
    {
        List<Employee> employees = new List<Employee>();

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string sql = @"SELECT * FROM Employee";
            using (var command = new SqlCommand(sql, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        {
                            EmpId = Convert.ToInt32(reader["EmpId"]),
                            FirstName = reader["FirstName"].ToString() ?? string.Empty,
                            LastName = reader["LastName"] == DBNull.Value ? null : reader["LastName"].ToString(),
                            DateOfBirth = reader["DateOfBirth"] == DBNull.Value ? null : (DateTime?)reader["DateOfBirth"],
                            Email = reader["Email"].ToString() ?? string.Empty,
                            Mobile = reader["Mobile"] == DBNull.Value ? null : reader["Mobile"].ToString(),
                            JoiningDate = Convert.ToDateTime(reader["JoiningDate"]),
                            RoleId = Convert.ToInt32(reader["RoleId"]),
                            Manager = reader["Manager"] == DBNull.Value ? null : reader["Manager"].ToString(),
                            Project = reader["Project"] == DBNull.Value ? null : reader["Project"].ToString()
                        });
                    }
                }
            }
        }
        return employees;
    }
    public Employee? GetEmployeeById(int empId)
    {
        Employee? employee = null;
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string sql = "SELECT * FROM Employee WHERE EmpId = @EmpId";

            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@EmpId", empId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        employee = new Employee
                        {
                            EmpId = Convert.ToInt32(reader["EmpId"]),
                            FirstName = Convert.ToString(reader["FirstName"]) ?? string.Empty,
                            LastName = reader["LastName"] != DBNull.Value ? Convert.ToString(reader["LastName"]) : null,
                            DateOfBirth = reader["DateOfBirth"] != DBNull.Value ? Convert.ToDateTime(reader["DateOfBirth"]) : (DateTime?)null,
                            Email = Convert.ToString(reader["Email"]) ?? string.Empty,
                            Mobile = reader["Mobile"] != DBNull.Value ? Convert.ToString(reader["Mobile"]) : null,
                            JoiningDate = Convert.ToDateTime(reader["JoiningDate"]),
                            RoleId = Convert.ToInt32(reader["RoleId"]),
                            Manager = reader["Manager"] != DBNull.Value ? Convert.ToString(reader["Manager"]) : null,
                            Project = reader["Project"] != DBNull.Value ? Convert.ToString(reader["Project"]) : null
                        };
                    }
                }
            }
        }
        return employee;
    }
    public void UpdateEmployee(Employee employee)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = @"UPDATE Employee SET 
                            FirstName = @FirstName,
                            LastName = @LastName,
                            DateOfBirth = @DateOfBirth,
                            Email = @Email,
                            Mobile = @Mobile,
                            JoiningDate = @JoiningDate,
                            RoleId = @RoleId,
                            Manager = @Manager,
                            Project = @Project
                         WHERE EmpId = @EmpId";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName", (object)employee.FirstName ?? DBNull.Value);
            command.Parameters.AddWithValue("@LastName", (object)employee.LastName ?? DBNull.Value);
            command.Parameters.AddWithValue("@DateOfBirth", (object)employee.DateOfBirth ?? DBNull.Value);
            command.Parameters.AddWithValue("@Email", (object)employee.Email ?? DBNull.Value);
            command.Parameters.AddWithValue("@Mobile", (object)employee.Mobile ?? DBNull.Value);
            command.Parameters.AddWithValue("@JoiningDate", employee.JoiningDate);
            command.Parameters.AddWithValue("@RoleId", employee.RoleId);
            command.Parameters.AddWithValue("@Manager", (object)employee.Manager ?? DBNull.Value);
            command.Parameters.AddWithValue("@Project", (object)employee.Project ?? DBNull.Value);
            command.Parameters.AddWithValue("@EmpId", employee.EmpId);
            command.ExecuteNonQuery();
        }
    }

    public void DeleteEmployee(int empId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "DELETE FROM Employee WHERE EmpId = @EmpId";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@EmpId", empId);
            command.ExecuteNonQuery();
        }
    }

    public List<Employee> GetEmployeesByRoleId(int roleId)
    {
        List<Employee> employees = new List<Employee>();

        string sql = "SELECT EmpId, FirstName, LastName, DateOfBirth, Email, Mobile, JoiningDate, Manager, Project FROM Employee WHERE RoleId = @RoleId";

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@RoleId", roleId);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee
                        {
                            EmpId = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            DateOfBirth = reader.IsDBNull(3) ? null : reader.GetDateTime(3),
                            Email = reader.GetString(4),
                            Mobile = reader.IsDBNull(5) ? null : reader.GetString(5),
                            JoiningDate = reader.GetDateTime(6),
                            Manager = reader.IsDBNull(7) ? null : reader.GetString(7),
                            Project = reader.IsDBNull(8) ? null : reader.GetString(8)
                        };
                        employees.Add(employee);
                    }
                }
            }
        }
        return employees;
    }

}
