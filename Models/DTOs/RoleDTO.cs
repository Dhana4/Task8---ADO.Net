using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EmployeeConsoleADO.Models;
public class RoleDTO
{
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public string Department { get; set; }
    public string? Description { get; set; }
    public string Location { get; set; }
}
