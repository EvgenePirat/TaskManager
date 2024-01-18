using BusinessLayer.Models.Roles.Response;

namespace BusinessLayer.Models.Users.Response
{
    /// <summary>
    /// Model for response user entity
    /// </summary>
    public class UserModel
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }

        public RoleModel Role { get; set; }

        public int Age { get; set; }

    }
}
