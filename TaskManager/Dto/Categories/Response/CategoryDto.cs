using TaskManager.Dto.Tasks.Response;

namespace TaskManager.Dto.Categories.Response
{
    /// <summary>
    /// DTO for response category entity
    /// </summary>
    public class CategoryDto
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public List<TaskDto>? Tasks { get; set; }

        public string? Color { get; set; }
    }
}
