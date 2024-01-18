using TaskManager.Dto.Roles.Response;

namespace TaskManager.Dto.Users.Response
{
    /// <summary>
    /// DTO for response user entity
    /// </summary>
    public class UserDto
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }

        public RoleDto Role { get; set; }

        public int Age { get; set; }

    }
}
