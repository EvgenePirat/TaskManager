using BusinessLayer.DTO.RoleDto.Response;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTO.UserDto.Response
{
    /// <summary>
    /// DTO for response user entity
    /// </summary>
    public class UserResponse
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }

        public RoleResponse? Role { get; set; }

        public int Age { get; set; }

    }
}
