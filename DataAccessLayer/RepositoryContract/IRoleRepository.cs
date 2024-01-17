using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.RepositoryContract
{
    /// <summary>
    /// Perpesents data access logic for managing Role entity
    /// </summary>
    public interface IRoleRepository
    {
        /// <summary>
        /// Method for gets all roles from basedata
        /// </summary>
        /// <returns>returned list with role from db</returns>
        public Task<List<Role>> GetAllRolesAsync();
    }
}
