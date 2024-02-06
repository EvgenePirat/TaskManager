using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Dto.Users.Request
{
    /// <summary>
    /// Dto class for restore password for user
    /// </summary>
    public class UserRestoreDto
    {
        [Required(ErrorMessage = "UserName can't be blank")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Email can't be blank")]
        [EmailAddress(ErrorMessage = "Email should be in a proper email address format")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
    }
}
