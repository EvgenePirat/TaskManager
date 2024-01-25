using System.ComponentModel.DataAnnotations;

namespace TaskManager.Dto.Categories.Request
{
    /// <summary>
    /// DTO for add new category entity
    /// </summary>
    public class CategoryAddDto
    {
        [Required(ErrorMessage = "Category name can't be blank")]
        public string? Name { get; set; }
    }
}
