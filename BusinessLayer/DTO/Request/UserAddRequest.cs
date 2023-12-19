using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTO.Request
{
    /// <summary>
    /// DTO for interesting a new user
    /// </summary>
    public class UserAddRequest
    {
        [Required(ErrorMessage = "User name can't be blank")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "UserName can't be blank")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "User email can't be blank")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password can't be blank")]
        public string? Password { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Guid RoleId { get; set; }
    }
}
