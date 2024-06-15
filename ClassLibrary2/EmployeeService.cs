using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeConsoleADO.Service.Interfaces;
using EmployeeConsoleADO.Data.Models;
using EmployeeConsoleADO.Data.Interfaces;
using EmployeeConsoleADO.Models;
using EmployeeConsoleADO.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AutoMapper;
using System.Reflection.Metadata.Ecma335;

namespace EmployeeConsoleADO.Service;
public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository employeeDataAccess;
    private readonly IMapper mapper;
    public EmployeeService(IEmployeeRepository employeeDataAccess,IMapper mapper)
    {
        this.employeeDataAccess = employeeDataAccess;
        this.mapper = mapper;
    }
    public void AddEmployee(EmployeeDTO employeeDTO)
    {
        Employee employee = mapper.Map<Employee>(employeeDTO);
        employeeDataAccess.AddEmployee(employee);
    }
    public List<EmployeeDTO> GetAllEmployees()
    {
        List<Employee> employees = employeeDataAccess.GetAllEmployees();
        return mapper.Map<List<EmployeeDTO>>(employees);
    }

    public EmployeeDTO? GetEmployeeById(int empId)
    {
        Employee employee = employeeDataAccess.GetEmployeeById(empId);
        return mapper.Map<EmployeeDTO>(employee);
    }

    public void UpdateEmployee(EmployeeDTO employeeDTO)
    {
        Employee employee = mapper.Map<Employee>(employeeDTO);
        employeeDataAccess.UpdateEmployee(employee);
    }

    public void DeleteEmployee(int empId)
    {
        employeeDataAccess.DeleteEmployee(empId);
    }

    public List<EmployeeDTO> GetEmployeesByRoleId(int roleId)
    {
        List<Employee> employees = employeeDataAccess.GetEmployeesByRoleId(roleId);
        return mapper.Map<List<EmployeeDTO>>(employees);
    }
}
