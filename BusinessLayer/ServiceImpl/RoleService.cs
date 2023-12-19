using BusinessLayer.DTO.Response;
using BusinessLayer.ServiceContract;
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
        public Task<List<RoleResponse>> GetAllRoles()
        {
            throw new NotImplementedException();
        }
    }
}
