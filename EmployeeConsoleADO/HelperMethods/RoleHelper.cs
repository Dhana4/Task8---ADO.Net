using EmployeeConsoleADO.Models;
using EmployeeConsoleADO.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmployeeConsoleADO.HelperMethods;
public class RoleHelper : IRoleHelper
{
    private readonly IRoleService roleManager;
    public RoleHelper(IRoleService roleManager)
    {
        this.roleManager = roleManager;
    }
    public void AddRole()
    {
        Console.WriteLine("Enter Role Name");
        bool roleNameEntered = false;
        string roleName = string.Empty;
        while (!roleNameEntered)
        {
            roleName = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(roleName) && Regex.IsMatch(roleName, "^[a-zA-Z ]+$"))
            {
                roleNameEntered = true;
            }
            else
            {
                Console.WriteLine("Invalid First Name! Please Enter again");
            }
        }
        Console.WriteLine("Enter Role Department Name");
        bool roleDepartmentEntered = false;
        string roleDepartmentName = string.Empty;
        while (!roleDepartmentEntered)
        {
            roleDepartmentName = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(roleDepartmentName) && Regex.IsMatch(roleDepartmentName, "^[a-zA-Z ]+$"))
            {
                roleDepartmentEntered = true;
            }
            else
            {
                Console.WriteLine("Invalid Department! Please Enter again");
            }
        }
        Console.WriteLine("Enter Role Description(Press Enter to skip)");
        string? roleDescription = null;
        roleDescription = Console.ReadLine();
        Console.WriteLine("Enter Employee Location");
        bool locationEntered = false;
        string location = string.Empty;
        while (!locationEntered)
        {
            location = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(location) && Regex.IsMatch(location, "^[a-zA-Z ]+$"))
            {
                locationEntered = true;
            }
            else
            {
                Console.WriteLine("Invalid Location! Please Enter again");
            }
        }

        var newRole = new RoleDTO
        {
            RoleName = roleName,
            Department = roleDepartmentName,
            Description = roleDescription,
            Location = location
        };
        roleManager.AddRole(newRole);
        Console.WriteLine("Role added successfully!");
    }
    public void EditRole()
    {

        DisplayAvailableRoles();
        int roleId = ChooseRoleId();
        RoleDTO role = roleManager.GetRoleById(roleId);
        Console.WriteLine("Do you want to edit Role Name(Y/N)");
        string roleNameToEdit = string.Empty;
        string roleNameOption = string.Empty;
        bool roleNameOptionEntered = false;
        bool roleNameToEditEntered = false;
        while (!roleNameOptionEntered)
        {
            roleNameOption = Console.ReadLine() ?? string.Empty;
            if (roleNameOption.ToLower()[0] == 'y' || roleNameOption.ToLower()[0] == 'n')
            {
                roleNameOptionEntered = true;
            }
            else
            {
                Console.WriteLine("Invalid Option entered! Please enter again");
            }
        }
        if (roleNameOption.ToLower()[0] == 'y')
        {
            Console.WriteLine("Enter Role Name to edit");
            while (!roleNameToEditEntered)
            {
                roleNameToEdit = Console.ReadLine() ?? string.Empty;
                if (Regex.IsMatch(roleNameToEdit, "^[a-zA-Z ]+$"))
                {
                    roleNameToEditEntered = true;
                }
                else
                {
                    Console.WriteLine("Invalid Role Name! Please Enter again");
                }
            }
        }
        else
        {
            roleNameToEdit = role.RoleName;
        }
        Console.WriteLine("Do you want to edit Department(Y/N)");
        string departmentToEdit = string.Empty;
        string departmentOption = string.Empty;
        bool departmentOptionEntered = false;
        bool departmentToEditEntered = false;
        while (!departmentOptionEntered)
        {
            departmentOption = Console.ReadLine() ?? string.Empty;
            if (departmentOption.ToLower()[0] == 'y' || departmentOption.ToLower()[0] == 'n')
            {
                departmentOptionEntered = true;
            }
            else
            {
                Console.WriteLine("Invalid Option entered! Please enter again");
            }
        }
        if (departmentOption.ToLower()[0] == 'y')
        {
            Console.WriteLine("Enter department to edit");
            while (!departmentToEditEntered)
            {
                departmentToEdit = Console.ReadLine() ?? string.Empty;
                if (Regex.IsMatch(departmentToEdit, "^[a-zA-Z ]+$"))
                {
                    departmentOptionEntered = true;
                }
                else
                {
                    Console.WriteLine("Invalid department! Please Enter again");
                }
            }
        }
        else
        {
            departmentToEdit = role.Department;
        }
        Console.WriteLine("Do you want to edit Description(Y/N)");
        string? descriptionToEdit = null;
        string descriptionOption = string.Empty;
        bool descriptionOptionEntered = false;
        while (!descriptionOptionEntered)
        {
            descriptionOption = Console.ReadLine() ?? string.Empty;
            if (descriptionOption.ToLower()[0] == 'y' || descriptionOption.ToLower()[0] == 'n')
            {
                descriptionOptionEntered = true;
            }
            else
            {
                Console.WriteLine("Invalid Option entered! Please enter again");
            }
        }
        if (descriptionOption.ToLower()[0] == 'y')
        {
            Console.WriteLine("Enter description to edit");
            descriptionToEdit = Console.ReadLine();
        }
        else
        {
            descriptionToEdit = role.Description;
        }
        Console.WriteLine("Do you want to edit Location(Y/N)");
        string locationToEdit = string.Empty;
        string locationOption = string.Empty;
        bool locationOptionEntered = false;
        bool locationToEditEntered = false;
        while (!locationOptionEntered)
        {
            locationOption = Console.ReadLine() ?? string.Empty;
            if (locationOption.ToLower()[0] == 'y' || locationOption.ToLower()[0] == 'n')
            {
                locationOptionEntered = true;
            }
            else
            {
                Console.WriteLine("Invalid Option entered! Please enter again");
            }
        }
        if (locationOption.ToLower()[0] == 'y')
        {
            Console.WriteLine("Enter location to edit");
            while (!locationToEditEntered)
            {
                locationToEdit = Console.ReadLine() ?? string.Empty;
                if (Regex.IsMatch(locationToEdit, "^[a-zA-Z ]+$"))
                {
                    locationToEditEntered = true;
                }
                else
                {
                    Console.WriteLine("Invalid location! Please Enter again");
                }
            }
        }
        else
        {
            locationToEdit = role.Location;
        }
        var updatedRole = new RoleDTO()
        {
            RoleId = roleId,
            RoleName = roleNameToEdit,
            Description = descriptionToEdit,
            Department = departmentToEdit,
            Location = locationToEdit
        };
        roleManager.EditRole(updatedRole);
        Console.WriteLine("Role Updated successfully!");

    }
    public int ChooseRoleId()
    {
        int roleId = default;
        string roleIdString = string.Empty;
        bool roleIdEntered = false;
        while (!roleIdEntered)
        {
            Console.WriteLine("Enter Role ID:");
            roleIdString = Console.ReadLine() ?? string.Empty;
            if (int.TryParse(roleIdString, out roleId))
            {
                if (roleManager.IsRoleIdValid(roleId))
                {
                    roleIdEntered = true;
                }
                else
                {
                    Console.WriteLine("Invalid role ID! Please enter a valid ID from the available roles.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input! Please enter a valid integer value for Role ID.");
            }
        }
        return roleId;
    }
    public bool DisplayAvailableRoles()
    {
        var roles = roleManager.GetAllRoles();
        if (roles.Count == 0)
        {
            Console.WriteLine("No Roles Exist. Please Add a role first");
            return false;
        }
        Console.WriteLine("Available Roles:");
        foreach (var role in roles)
        {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine($"ID: {role.RoleId}\n, Role Name: {role.RoleName}\nDescription: {role.Description}\nLocation: {role.Location}\nDepartment: {role.Department}");
        }
        return true;
    }
}
