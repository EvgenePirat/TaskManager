using BusinessLayer.DTO.RoleDto.Response;
using BusinessLayer.Mapper;
using BusinessLayer.ServiceContract;
using DataAccessLayer.RepositoryContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ServiceImpl
{
    /// <summary>
    /// Class for implementation methods for Role Service
    /// </summary>
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// method for return all roles
        /// </summary>
        /// <returns>returned all roles</returns>
        public async Task<List<RoleResponse>> GetAllRoles()
        {
            return (await _roleRepository.GetAllRolesAsync()).Select(role => RoleMapper.RoleToRoleResponse(role)).ToList();
        }
    }
}
