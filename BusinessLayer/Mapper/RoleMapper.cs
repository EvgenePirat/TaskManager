using BusinessLayer.DTO.RoleDto.Response;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Mapper
{
    /// <summary>
    /// Class mapper allows change class on other class with save data for role entity
    /// </summary>
    public static class RoleMapper
    {
        /// <summary>
        /// Method with logic for change from role class to roleResponse class
        /// </summary>
        /// <param name="role">role has data for roleresponse</param>
        /// <returns>returned roleresponse with data from role</returns>
        public static RoleModel RoleToRoleResponse(Role role)
        {
            return new RoleModel() { Id = role.Id, Name = role.Name };
        }
    }
}
