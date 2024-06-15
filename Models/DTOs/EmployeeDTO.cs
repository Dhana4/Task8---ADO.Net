using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeConsoleADO.Models;
public class EmployeeDTO
{
    public int EmpId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string Email { get; set; } = null!;

    public string? Mobile { get; set; }

    public DateTime JoiningDate { get; set; }

    public int RoleId { get; set; }

    public string? Manager { get; set; }

    public string? Project { get; set; }
}
