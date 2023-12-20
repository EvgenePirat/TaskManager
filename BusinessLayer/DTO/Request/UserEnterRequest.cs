using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTO.Request
{
    /// <summary>
    /// DTO for enter in system
    /// </summary>
    public class UserEnterRequest
    {
        [Required(ErrorMessage = "UserName can't be blank")]
        [StringLength(40, ErrorMessage = "Length can't be more then 40 chars")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password can't be blank")]
        [StringLength(30, ErrorMessage = "Length can't be more then 30 chars")]
        public string? Password { get; set; }
    }
}
