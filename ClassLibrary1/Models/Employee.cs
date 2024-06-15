using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeConsoleADO.Data.Models;

public class Employee
{
    public int EmpId { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string Email { get; set; }
    public string? Mobile { get; set; }
    public DateTime JoiningDate { get; set; }
    public int RoleId { get; set; }
    public string? Manager { get; set; }
    public string? Project { get; set; }

}
