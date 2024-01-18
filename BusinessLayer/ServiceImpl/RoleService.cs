using AutoMapper;
using BusinessLayer.Models.Roles.Response;
using BusinessLayer.ServiceContract;
using DataAccessLayer.RepositoryContract;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.ServiceImpl
{
    /// <summary>
    /// Class for implementation methods for Role Service
    /// </summary>
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<RoleService> _logger;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, ILogger<RoleService> logger, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// method for return all roles
        /// </summary>
        /// <returns>returned all roles</returns>
        public async Task<List<RoleModel>> GetAllRolesAsync()
        {
            _logger.LogInformation("{service}.{method} - start, get all roles in service layer", nameof(RoleService), nameof(GetAllRolesAsync));

            var roles = _mapper.Map<List<RoleModel>>(await _roleRepository.GetAllRolesAsync());

            _logger.LogInformation("{service}.{method} - finish, get all roles in service layer", nameof(RoleService), nameof(GetAllRolesAsync));

            return roles;
        }
    }
}
