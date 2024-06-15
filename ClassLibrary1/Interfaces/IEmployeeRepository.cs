using EmployeeConsoleADO.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeConsoleADO.Data.Interfaces;

public interface IEmployeeRepository
{
    void AddEmployee(Employee employee);
    List<Employee> GetAllEmployees();
    Employee? GetEmployeeById(int empId);
    void UpdateEmployee(Employee employee);
    void DeleteEmployee(int empId);
    List<Employee> GetEmployeesByRoleId(int roleId);
}
