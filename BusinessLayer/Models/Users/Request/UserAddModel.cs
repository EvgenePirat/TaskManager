using BusinessLayer.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.Users.Request
{
    /// <summary>
    /// Model for interesting a new user
    /// </summary>
    public class UserAddModel
    {
        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public DateTime DateOfBirth { get; set; }

        public UserTypes UserType { get; set; } = UserTypes.User;
    }
}
