using BusinessLayer.Models.Enum;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Dto.Users.Request
{
    /// <summary>
    /// DTO for interesting a new user
    /// </summary>
    public class UserAddDto
    {
        [Required(ErrorMessage = "UserName can't be blank")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Email can't be blank")]
        [EmailAddress(ErrorMessage = "Email should be in a proper email address format")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password can't be blank")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirm Password can't be blank")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirm password do not match")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Date of birth can't be blank")]
        public DateTime DateOfBirth { get; set; }

        public UserTypes UserType { get; set; } = UserTypes.User;
    }
}
