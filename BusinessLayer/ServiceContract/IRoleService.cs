using BusinessLayer.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ServiceContract
{
    /// <summary>
    /// Rerpesents business logic for manipoulating role entity
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Method for get all roles
        /// </summary>
        /// <returns>return a list of objects of roleresponse type</returns>
        public Task<List<RoleResponse>> GetAllRoles();
    }
}
