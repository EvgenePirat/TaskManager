using System.ComponentModel.DataAnnotations;

namespace TaskManager.Dto.Users.Request
{
    /// <summary>
    /// DTO for interesting a new user
    /// </summary>
    public class UserAddDto
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

        [Required(ErrorMessage = "Date of birth can't be blank")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Role id can't be blank")]
        public Guid RoleId { get; set; }
    }
}
