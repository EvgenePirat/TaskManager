using System.ComponentModel.DataAnnotations;

namespace TaskManager.Dto.Roles.Request
{
    /// <summary>
    /// DTO for add new role entity
    /// </summary>
    public class RoleAddDto
    {
        [Required(ErrorMessage = "Role name can be")]
        public string? Name { get; set; }
    }
}
