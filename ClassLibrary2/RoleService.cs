using EmployeeConsoleADO.Models;
using Microsoft.Extensions.DependencyInjection;
using EmployeeConsoleADO.Data.Models;
using EmployeeConsoleADO.Service.Interfaces;
using EmployeeConsoleADO.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace EmployeeConsoleADO.Service;
public class RoleService : IRoleService
{
    private readonly IRoleRepository roleDataAccess;
    private readonly IMapper mapper;
    public RoleService(IRoleRepository roleDataAccess, IMapper mapper)
    {
        this.roleDataAccess = roleDataAccess;
        this.mapper = mapper;
    }
    public List<RoleDTO> GetAllRoles()
    {
        List<Role> roles = roleDataAccess.GetAllRoles();
        return mapper.Map<List<RoleDTO>>(roles);
        
    }
    public void AddRole(RoleDTO roleDTO)
    {
        Role role = mapper.Map<Role>(roleDTO);
        roleDataAccess.AddRole(role);
    }

    public RoleDTO GetRoleById(int roleId)
    {
        Role role =  roleDataAccess.GetRoleById(roleId);
        return mapper.Map<RoleDTO>(role);
    }

    public void EditRole(RoleDTO updatedRoleDTO)
    {
        Role role = mapper.Map<Role>(updatedRoleDTO);
        roleDataAccess.EditRole(role);
    }
    public bool IsRoleIdValid(int roleId)
    {
        return roleDataAccess.IsRoleIdValid(roleId);
    }
}
