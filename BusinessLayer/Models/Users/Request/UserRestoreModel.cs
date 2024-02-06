using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.Users.Request
{
    /// <summary>
    /// Model for reset password for user
    /// </summary>
    public class UserRestoreModel
    {
        public string? UserName { get; set; }

        public string? Email { get; set; }
    }
}
