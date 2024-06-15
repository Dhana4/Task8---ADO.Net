using EmployeeConsoleADO.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeConsoleADO.Data.Interfaces;

public interface IRoleRepository
{
    List<Role> GetAllRoles();
    void AddRole(Role role);
    Role GetRoleById(int roleId);
    void EditRole(Role updatedRole);
    bool IsRoleIdValid(int roleId);
}
