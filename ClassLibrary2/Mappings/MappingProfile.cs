using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeConsoleADO.Data.Models;
using EmployeeConsoleADO.Models;
namespace EmployeeConsoleADO.Service.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<EmployeeDTO, Employee>();
        CreateMap<Employee, EmployeeDTO>();
        CreateMap<RoleDTO, Role>();
        CreateMap<Role, RoleDTO>();
    }
}
