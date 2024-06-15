using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeConsoleADO.HelperMethods;
public interface IRoleHelper
{
    void AddRole();
    void EditRole();
    int ChooseRoleId();
    bool DisplayAvailableRoles();
}
