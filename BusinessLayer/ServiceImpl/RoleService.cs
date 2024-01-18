using BusinessLayer.DTO.RoleDto.Response;
using BusinessLayer.Mapper;
using BusinessLayer.Models.Roles.Response;
using BusinessLayer.ServiceContract;
using DataAccessLayer.RepositoryContract;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<RoleService> _logger;

        public RoleService(IRoleRepository roleRepository, ILogger<RoleService> logger)
        {
            _roleRepository = roleRepository;
            _logger = logger;
        }

        /// <summary>
        /// method for return all roles
        /// </summary>
        /// <returns>returned all roles</returns>
        public async Task<List<RoleModel>> GetAllRolesAsync()
        {
            _logger.LogInformation("{service}.{method} - start, get all roles in service layer", nameof(RoleService), nameof(GetAllRolesAsync));

            var roles = (await _roleRepository.GetAllRolesAsync()).Select(role => RoleMapper.RoleToRoleResponse(role)).ToList();

            _logger.LogInformation("{service}.{method} - finish, get all roles in service layer", nameof(RoleService), nameof(GetAllRolesAsync));

            return roles;
        }
    }
}
