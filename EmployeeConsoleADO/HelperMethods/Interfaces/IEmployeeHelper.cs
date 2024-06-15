using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeConsoleADO.HelperMethods;
internal interface IEmployeeHelper
{
    void AddEmployee();
    void DisplayOneEmployee();
    void EditEmployee();
    void DeleteEmployee();
    void DisplayAllEmployees();
    void ViewAllEmpInRole();
}
