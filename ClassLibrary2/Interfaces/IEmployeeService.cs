using EmployeeConsoleADO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeConsoleADO.Service.Interfaces;
public interface IEmployeeService
{
    void AddEmployee(EmployeeDTO employee);
    List<EmployeeDTO> GetAllEmployees();
    EmployeeDTO? GetEmployeeById(int empId);
    void UpdateEmployee(EmployeeDTO employee);
    void DeleteEmployee(int empId);
    List<EmployeeDTO> GetEmployeesByRoleId(int roleId);
}
