using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EmployeeConsoleADO.Service.Interfaces;
using EmployeeConsoleADO.Models;

namespace EmployeeConsoleADO.HelperMethods;

public class EmployeeHelper : IEmployeeHelper
{
    private readonly IEmployeeService employeeManager;
    private readonly IRoleHelper roleHelper;
    private readonly IRoleService roleManager;
    public EmployeeHelper(IEmployeeService employeeManager, IRoleHelper roleHelper, IRoleService roleManager)
    {
        this.employeeManager = employeeManager;
        this.roleHelper = roleHelper;
        this.roleManager = roleManager;
    }
    public void AddEmployee()
    {
        Console.WriteLine("Enter Employee First Name");
        bool firstNameEntered = false;
        string firstName = string.Empty;
        while (!firstNameEntered)
        {
            firstName = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(firstName) && Regex.IsMatch(firstName, "^[a-zA-Z ]+$"))
            {
                firstNameEntered = true;
            }
            else
            {
                Console.WriteLine("Invalid First Name! Please Enter again");
            }
        }
        Console.WriteLine("Enter Employee Last Name or (press enter to skip)");
        bool lastNameEntered = false;
        string? lastName = null;
        while (!lastNameEntered)
        {
            lastName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(lastName))
            {
                lastNameEntered = true;
            }
            else if (Regex.IsMatch(lastName, "^[a-zA-Z ]+$"))
            {
                lastNameEntered = true;
            }
            else
            {
                Console.WriteLine("Invalid last Name! Please Enter again (Press Enter to skip)");
            }
        }
        Console.WriteLine("Enter Employee Date Of Birth (mm/dd/yyyy) or (Press Enter to skip)");
        DateTime? dob = null;
        string dobString;
        bool dobEntered = false;
        while (!dobEntered)
        {
            dobString = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(dobString))
            {
                dobEntered = true;
                break;
            }
            if (DateTime.TryParse(dobString, out DateTime tempDob))
            {
                dob = tempDob;
                dobEntered = true;
            }
            else
            {
                Console.WriteLine("Invalid Date of birth, please enter again, (Press Enter to skip)");
            }
        }
        Console.WriteLine("Enter Employee Email");
        string email = string.Empty;
        bool emailEntered = false;
        while (!emailEntered)
        {
            email = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(email) && Regex.IsMatch(email, "[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}"))
            {
                emailEntered = true;
            }
            else
            {
                Console.WriteLine("Invalid Email, Please enter again");
            }
        }

        Console.WriteLine("Enter Employee Mobile (Press Enter to Skip)");
        string? mobile = null;
        bool mobileEntered = false;
        while (!mobileEntered)
        {
            mobile = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(mobile))
            {
                mobileEntered = true;
            }
            else if (Regex.IsMatch(mobile, "^\\d{10}$|^\\+\\d{2}\\d{10}$"))
            {
                mobileEntered = true;
            }
            else
            {
                Console.WriteLine("Invalid mobile! Please enter again, (Press Enter to skip)");
            }
        }
        Console.WriteLine("Enter Employee Joining Date (mm/dd/yyyy)");
        DateTime joinDate = default;
        string? joinDateString;
        bool joinDateEntered = false;
        while (!joinDateEntered)
        {
            joinDateString = Console.ReadLine();
            if (DateTime.TryParse(joinDateString, out joinDate))
            {
                joinDateEntered = true;
            }
            else
            {
                Console.WriteLine("Invalid joining Date! Please Enter again");
            }
        }
        bool rolesExist = roleHelper.DisplayAvailableRoles();
        if (rolesExist == false)
        {
            return;
        }
        int roleId = roleHelper.ChooseRoleId();
        Console.WriteLine("Enter Employee Manager Name or (Press Enter to skip)");
        bool managerEntered = false;
        string? manager = null;
        while (!managerEntered)
        {
            manager = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(manager))
            {
                managerEntered = true;
            }
            else if (Regex.IsMatch(manager, "^[a-zA-Z]+$"))
            {
                managerEntered = true;
            }
            else
            {
                Console.WriteLine("Invalid Manager name! please enter again, (Press Enter to skip)");
            }
        }
        Console.WriteLine("Enter Employee Project Name or (Press Enter to skip)");
        string? project = null;
        bool projectEntered = false;
        while (!projectEntered)
        {
            project = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(project))
            {
                projectEntered = true;
            }
            else if (Regex.IsMatch(project, "^[a-zA-Z]+$"))
            {
                projectEntered = true;
            }
            else
            {
                Console.WriteLine("Invalid Project name! Please Enter again, (Press Enter to skip)");
            }
        }
        var newEmployee = new EmployeeDTO
        {
            FirstName = firstName,
            LastName = lastName,
            DateOfBirth = dob,
            Email = email,
            Mobile = mobile,
            JoiningDate = joinDate,
            RoleId = roleId,
            Manager = manager,
            Project = project
        };
        employeeManager.AddEmployee(newEmployee);
        Console.WriteLine("Employee added successfully!");
    }

    public void DisplayOneEmployee()
    {
        Console.WriteLine("Enter Employee ID:");
        int empId = default;
        if (int.TryParse(Console.ReadLine(), out empId))
        {
            var employee = employeeManager.GetEmployeeById(empId);
            if (employee != null)
            {
                Console.WriteLine($"Employee Details for ID {employee.EmpId}:");
                Console.WriteLine($"First Name: {employee.FirstName}");
                Console.WriteLine($"Last Name: {employee.LastName ?? "N/A"}");
                Console.WriteLine($"Date of Birth: {employee.DateOfBirth?.ToShortDateString() ?? "N/A"}");
                Console.WriteLine($"Email: {employee.Email}");
                Console.WriteLine($"Mobile: {employee.Mobile ?? "N/A"}");
                Console.WriteLine($"Joining Date: {employee.JoiningDate.ToShortDateString()}");
                var role = roleManager.GetRoleById(employee.RoleId);
                Console.WriteLine($"Role Name: {role.RoleName}");
                Console.WriteLine($"Department: {role.Department}");
                Console.WriteLine($"Location: {role.Location}");
                Console.WriteLine($"Manager: {employee.Manager ?? "N/A"}");
                Console.WriteLine($"Project: {employee.Project ?? "N/A"}");
            }
            else
            {
                Console.WriteLine($"Employee with ID {empId} not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input! Please enter a valid integer value for Employee ID.");
        }
    }

    public void EditEmployee()
    {
        Console.WriteLine("Enter Employee ID to edit:");
        int empId = default;
        bool empIdEntered = false;
        while (!empIdEntered)
        {
            if (int.TryParse(Console.ReadLine(), out empId))
            {
                empIdEntered = true;
                var employee = employeeManager.GetEmployeeById(empId);
                if (employee != null)
                {
                    Console.WriteLine($"Employee Details for ID {employee.EmpId}:");
                    Console.WriteLine($"First Name: {employee.FirstName}");
                    Console.WriteLine($"Last Name: {employee.LastName ?? "N/A"}");
                    Console.WriteLine($"Date of Birth: {employee.DateOfBirth?.ToShortDateString() ?? "N/A"}");
                    Console.WriteLine($"Email: {employee.Email}");
                    Console.WriteLine($"Mobile: {employee.Mobile ?? "N/A"}");
                    Console.WriteLine($"Joining Date: {employee.JoiningDate.ToShortDateString()}");
                    Console.WriteLine($"Role ID: {employee.RoleId}");
                    Console.WriteLine($"Manager: {employee.Manager ?? "N/A"}");
                    Console.WriteLine($"Project: {employee.Project ?? "N/A"}");
                    Console.WriteLine("Do you want to edit First Name(Y/N)");
                    string firstNameOption = string.Empty;
                    bool firstNameOptionEntered = false;
                    string firstNameToEdit = string.Empty;
                    bool firstNameToEditEntered = false;
                    while (!firstNameOptionEntered)
                    {
                        firstNameOption = Console.ReadLine() ?? string.Empty;
                        if (firstNameOption.ToLower()[0] == 'y' || firstNameOption.ToLower()[0] == 'n')
                        {
                            firstNameOptionEntered = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Choice! Please Enter again");
                        }
                    }
                    if (firstNameOption.ToLower()[0] == 'y')
                    {
                        Console.WriteLine("Enter First Name to edit");
                        while (!firstNameToEditEntered)
                        {
                            firstNameToEdit = Console.ReadLine() ?? string.Empty;
                            if (Regex.IsMatch(firstNameToEdit, "^[a-zA-Z ]+$"))
                            {
                                firstNameToEditEntered = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid First Name! Please Enter again");
                            }
                        }
                    }
                    else
                    {
                        firstNameToEdit = employee.FirstName;
                    }

                    Console.WriteLine("Do you want to edit Last Name(Y/N)");
                    string lastNameOption = string.Empty;
                    bool lastNameOptionEntered = false;
                    string? lastNameToEdit = null;
                    bool lastNameToEditEntered = false;
                    while (!lastNameOptionEntered)
                    {
                        lastNameOption = Console.ReadLine() ?? string.Empty;
                        if (lastNameOption.ToLower()[0] == 'y' || lastNameOption.ToLower()[0] == 'n')
                        {
                            lastNameOptionEntered = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Choice! Please Enter again");
                        }
                    }
                    if (lastNameOption.ToLower()[0] == 'y')
                    {
                        Console.WriteLine("Enter last Name to edit");
                        while (!lastNameToEditEntered)
                        {
                            lastNameToEdit = Console.ReadLine() ?? string.Empty;
                            if (Regex.IsMatch(lastNameToEdit, "^[a-zA-Z ]+$"))
                            {
                                lastNameToEditEntered = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid last Name! Please Enter again");
                            }
                        }
                    }
                    else
                    {
                        lastNameToEdit = employee.LastName;
                    }
                    Console.WriteLine("Do you want to edit Date of birth(Y/N)");
                    string dobOption = string.Empty;
                    bool dobOptionEntered = false;
                    DateTime? dobToEdit = null;
                    bool dobToEditEntered = false;
                    while (!dobOptionEntered)
                    {
                        dobOption = Console.ReadLine() ?? string.Empty;
                        if (dobOption.ToLower()[0] == 'y' || dobOption.ToLower()[0] == 'n')
                        {
                            dobOptionEntered = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Choice! Please Enter again");
                        }
                    }
                    if (dobOption.ToLower()[0] == 'y')
                    {
                        Console.WriteLine("Enter Date of birth to edit");
                        while (!dobToEditEntered)
                        {
                            if (DateTime.TryParse(Console.ReadLine(), out DateTime tempDb))
                            {
                                dobToEdit = tempDb;
                                dobToEditEntered = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Date of birth! Please Enter again");
                            }
                        }
                    }
                    else
                    {
                        dobToEdit = employee.DateOfBirth;
                    }
                    Console.WriteLine("Do you want to edit Email(Y/N)");
                    string emailOption = string.Empty;
                    bool emailOptionEntered = false;
                    string emailToEdit = string.Empty;
                    bool emailToEditEntered = false;
                    while (!emailOptionEntered)
                    {
                        emailOption = Console.ReadLine() ?? string.Empty;
                        if (emailOption.ToLower()[0] == 'y' || emailOption.ToLower()[0] == 'n')
                        {
                            emailOptionEntered = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Choice! Please Enter again");
                        }
                    }
                    if (emailOption.ToLower()[0] == 'y')
                    {
                        Console.WriteLine("Enter email to edit");
                        while (!emailToEditEntered)
                        {
                            emailToEdit = Console.ReadLine() ?? string.Empty;
                            if (Regex.IsMatch(emailToEdit, "[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}"))
                            {
                                emailToEditEntered = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Email! Please Enter again");
                            }
                        }
                    }
                    else
                    {
                        emailToEdit = employee.Email;
                    }
                    Console.WriteLine("Do you want to edit Mobile(Y/N)");
                    string mobileOption = string.Empty;
                    bool mobileOptionEntered = false;
                    string? mobileToEdit = null;
                    bool mobileToEditEntered = false;
                    while (!mobileOptionEntered)
                    {
                        mobileOption = Console.ReadLine() ?? string.Empty;
                        if (mobileOption.ToLower()[0] == 'y' || mobileOption.ToLower()[0] == 'n')
                        {
                            mobileOptionEntered = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Choice! Please Enter again");
                        }
                    }
                    if (mobileOption.ToLower()[0] == 'y')
                    {
                        Console.WriteLine("Enter mobile to edit");
                        while (!mobileToEditEntered)
                        {
                            mobileToEdit = Console.ReadLine() ?? string.Empty;
                            if (Regex.IsMatch(mobileToEdit, "^\\d{10}$|^\\+\\d{2}\\d{10}$"))
                            {
                                mobileToEditEntered = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid mobile! Please Enter again");
                            }
                        }
                    }
                    else
                    {
                        mobileToEdit = employee.Mobile;
                    }
                    Console.WriteLine("Do you want to edit Joining Date(Y/N)");
                    string joiningDateOption = string.Empty;
                    bool joiningDateOptionEntered = false;
                    DateTime joiningDateToEdit = default;
                    bool joiningDateToEditEntered = false;
                    while (!joiningDateOptionEntered)
                    {
                        joiningDateOption = Console.ReadLine() ?? string.Empty;
                        if (joiningDateOption.ToLower()[0] == 'y' || joiningDateOption.ToLower()[0] == 'n')
                        {
                            joiningDateOptionEntered = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Choice! Please Enter again");
                        }
                    }
                    if (joiningDateOption.ToLower()[0] == 'y')
                    {
                        Console.WriteLine("Enter Date of birth to edit");
                        while (!joiningDateToEditEntered)
                        {
                            if (DateTime.TryParse(Console.ReadLine(), out joiningDateToEdit))
                            {
                                joiningDateToEditEntered = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Joining Date! Please Enter again");
                            }
                        }
                    }
                    else
                    {
                        joiningDateToEdit = employee.JoiningDate;
                    }
                    Console.WriteLine("Do you want to edit role Id(Y/N)");
                    string roleIdOption = string.Empty;
                    bool roleIdOptionEntered = false;
                    int roleIdToEdit = default;
                    bool roleIdToEditEntered = false;
                    while (!roleIdOptionEntered)
                    {
                        roleIdOption = Console.ReadLine() ?? string.Empty;
                        if (roleIdOption.ToLower()[0] == 'y' || roleIdOption.ToLower()[0] == 'n')
                        {
                            roleIdOptionEntered = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Choice! Please Enter again");
                        }
                    }
                    if (roleIdOption.ToLower()[0] == 'y')
                    {
                        roleHelper.DisplayAvailableRoles();
                        Console.WriteLine("Enter roleId to Assign");
                        while (!roleIdToEditEntered)
                        {
                            if (int.TryParse(Console.ReadLine(), out roleIdToEdit) && roleManager.IsRoleIdValid(roleIdToEdit))
                            {
                                roleIdToEditEntered = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid roleId! Please Enter again");
                            }
                        }
                    }
                    else
                    {
                        roleIdToEdit = employee.RoleId;
                    }
                    Console.WriteLine("Do you want to edit Manager Name(Y/N)");
                    string managerNameOption = string.Empty;
                    bool managerNameOptionEntered = false;
                    string? managerNameToEdit = null;
                    bool managerNameToEditEntered = false;
                    while (!managerNameOptionEntered)
                    {
                        managerNameOption = Console.ReadLine() ?? string.Empty;
                        if (managerNameOption.ToLower()[0] == 'y' || managerNameOption.ToLower()[0] == 'n')
                        {
                            managerNameOptionEntered = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Choice! Please Enter again");
                        }
                    }
                    if (managerNameOption.ToLower()[0] == 'y')
                    {
                        Console.WriteLine("Enter last Name to edit");
                        while (!managerNameToEditEntered)
                        {
                            managerNameToEdit = Console.ReadLine() ?? string.Empty;
                            if (Regex.IsMatch(managerNameToEdit, "^[a-zA-Z ]+$"))
                            {
                                managerNameToEditEntered = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Manager Name! Please Enter again");
                            }
                        }
                    }
                    else
                    {
                        managerNameToEdit = employee.Manager;
                    }
                    Console.WriteLine("Do you want to edit Project(Y/N)");
                    string projectNameOption = string.Empty;
                    bool projectNameOptionEntered = false;
                    string? projectNameToEdit = null;
                    bool projectNameToEditEntered = false;
                    while (!projectNameOptionEntered)
                    {
                        projectNameOption = Console.ReadLine() ?? string.Empty;
                        if (projectNameOption.ToLower()[0] == 'y' || projectNameOption.ToLower()[0] == 'n')
                        {
                            projectNameOptionEntered = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Choice! Please Enter again");
                        }
                    }
                    if (projectNameOption.ToLower()[0] == 'y')
                    {
                        Console.WriteLine("Enter last Name to edit");
                        while (!projectNameToEditEntered)
                        {
                            projectNameToEdit = Console.ReadLine() ?? string.Empty;
                            if (Regex.IsMatch(projectNameToEdit, "^[a-zA-Z ]+$"))
                            {
                                projectNameToEditEntered = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid project Name! Please Enter again");
                            }
                        }
                    }
                    else
                    {
                        projectNameToEdit = employee.Project;
                    }
                    var updatedEmployee = new EmployeeDTO
                    {
                        EmpId = empId,
                        FirstName = firstNameToEdit,
                        LastName = lastNameToEdit,
                        DateOfBirth = dobToEdit,
                        Email = emailToEdit,
                        Mobile = mobileToEdit,
                        JoiningDate = joiningDateToEdit,
                        RoleId = roleIdToEdit,
                        Manager = managerNameToEdit,
                        Project = projectNameToEdit
                    };
                    employeeManager.UpdateEmployee(updatedEmployee);
                    Console.WriteLine("Employee details updated successfully!");
                }
                else
                {
                    Console.WriteLine($"Employee with ID {empId} not found! Please Enter again");
                }
            }
            else
            {
                Console.WriteLine("Invalid input! Please enter a valid integer value for Employee ID.");
            }
        }
    }

    public void DeleteEmployee()
    {
        Console.WriteLine("Enter Employee ID to delete:");
        int empIdToDelete = 0;
        bool empIdToDeleteEntered = false;
        while (!empIdToDeleteEntered)
        {
            if (int.TryParse(Console.ReadLine(), out empIdToDelete))
            {
                var employee = employeeManager.GetEmployeeById(empIdToDelete);
                if (employee != null)
                {
                    empIdToDeleteEntered = true;
                    Console.WriteLine($"Are you sure you want to delete employee {employee.FirstName} {employee.LastName}? (Y/N)");
                    string confirmation = Console.ReadLine()?.Trim().ToLower() ?? string.Empty;
                    if (confirmation[0] == 'y')
                    {
                        employeeManager.DeleteEmployee(empIdToDelete);
                        Console.WriteLine($"Employee {employee.FirstName} {employee.LastName} deleted successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Deletion canceled.");
                    }
                }
                else
                {
                    Console.WriteLine($"Employee with ID {empIdToDelete} not found!");
                    empIdToDeleteEntered = true;
                }
            }
            else
            {
                Console.WriteLine("Invalid input! Please enter again.");
            }
        }
    }

    public void DisplayAllEmployees()
    {
        var employees = employeeManager.GetAllEmployees();
        if (employees.Count == 0)
        {
            Console.WriteLine("No Employees to display");
            return;
        }
        Console.WriteLine("List of Employees:");
        foreach (var employee in employees)
        {
            Console.WriteLine($"ID: {employee.EmpId} \nFull Name: {employee.FirstName} {employee.LastName ?? ""} \nEmail: {employee.Email}\nMobile: {employee.Mobile ?? "N/A"}\nJoining Date: {employee.JoiningDate.ToShortDateString()}\nManager: {employee.Manager ?? "N/A"}\nProject: {employee.Project ?? "N/A"}");
            var role = roleManager.GetRoleById(employee.RoleId);
            Console.WriteLine($"Role Name: {role.RoleName}\nDepartment: {role.Department}\nLocation: {role.Location}\nRole Description{role.Description ?? ""}\n");
        }
    }
    public void ViewAllEmpInRole()
    {
        Console.WriteLine("Enter Role ID:");
        if (int.TryParse(Console.ReadLine(), out int roleId) && roleManager.IsRoleIdValid(roleId))
        {
            List<EmployeeDTO> employeesInRole = employeeManager.GetEmployeesByRoleId(roleId);

            if (employeesInRole.Count > 0)
            {
                Console.WriteLine($"Employees in Role with ID {roleId}:");
                foreach (var employee in employeesInRole)
                {
                    Console.WriteLine($"Employee ID: {employee.EmpId}, Name: {employee.FirstName} {employee.LastName}");
                }
            }
            else
            {
                Console.WriteLine($"No employees found in Role with ID {roleId}");
            }
        }
        else
        {
            Console.WriteLine("Invalid Role ID entered.");
        }
    }
}

