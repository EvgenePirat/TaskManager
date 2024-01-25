using System.ComponentModel.DataAnnotations;

namespace TaskManager.Dto.Categories.Request
{
    /// <summary>
    /// DTO for update exist category entity
    /// </summary>
    public class CategoryUpdateDto
    {
        [Required(ErrorMessage = "Category id can't be blank")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Category old name can't be blank")]
        public string? OldName { get; set; }

        [Required(ErrorMessage = "Category new name can't be blank")]
        public string? NewName { get; set; }
    }
}
