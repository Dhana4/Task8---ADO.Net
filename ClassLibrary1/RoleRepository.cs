using EmployeeConsoleADO.Data.Interfaces;
using EmployeeConsoleADO.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeConsoleADO.Data;

public class RoleRepository : IRoleRepository
{
    private readonly string connectionString;

    public RoleRepository(string _connectionString)
    {
        connectionString = _connectionString;
    }
    public List<Role> GetAllRoles()
    {
        List<Role> roles = new List<Role>();

        string sql = "SELECT RoleId, RoleName, Department, Description, Location FROM Role";

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand(sql, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Role role = new Role
                        {
                            RoleId = reader.GetInt32(0),
                            RoleName = reader.GetString(1),
                            Department = reader.GetString(2),
                            Description = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Location = reader.GetString(4)
                        };
                        roles.Add(role);
                    }
                }
            }
        }
        return roles;
    }
    public void AddRole(Role role)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = @"INSERT INTO Role (RoleName, Department, Description, Location) 
                                 VALUES (@RoleName, @Department, @Description, @Location)";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@RoleName", role.RoleName);
            command.Parameters.AddWithValue("@Department", role.Department);
            command.Parameters.AddWithValue("@Description", (object)role.Description ?? DBNull.Value);
            command.Parameters.AddWithValue("@Location", role.Location);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
    public Role GetRoleById(int roleId)
    {
        Role? role = new Role();

        string sql = "SELECT RoleId, RoleName, Department, Description, Location FROM Role WHERE RoleId = @RoleId";

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@RoleId", roleId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        role = new Role
                        {
                            RoleId = reader.GetInt32(0),
                            RoleName = reader.GetString(1),
                            Department = reader.GetString(2),
                            Description = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Location = reader.GetString(4)
                        };
                    }
                }
            }
        }
        return role;
    }

    public void EditRole(Role updatedRole)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = @"UPDATE Role 
                        SET RoleName = @RoleName, 
                            Department = @Department, 
                            Description = @Description, 
                            Location = @Location 
                        WHERE RoleId = @RoleId";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@RoleName", updatedRole.RoleName);
            command.Parameters.AddWithValue("@Department", updatedRole.Department);
            command.Parameters.AddWithValue("@Description", (object)updatedRole.Description ?? DBNull.Value);
            command.Parameters.AddWithValue("@Location", updatedRole.Location);
            command.Parameters.AddWithValue("@RoleId", updatedRole.RoleId);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
    public bool IsRoleIdValid(int roleId)
    {
        bool isRoleValid = false;
        var roles = GetAllRoles();
        foreach (var role in roles)
        {
            if (role.RoleId == roleId)
            {
                isRoleValid = true;
                break;
            }
        }
        return isRoleValid;
    }

}
