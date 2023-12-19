using BusinessLayer.DTO.Response;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Mapper
{
    public static class RoleMapper
    {
        public static RoleResponse RoleToRoleResponse(Role role)
        {
            return new RoleResponse() { Id = role.Id, Name = role.Name };
        }
    }
}
