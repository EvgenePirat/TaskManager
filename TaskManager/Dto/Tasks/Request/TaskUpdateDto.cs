using BusinessLayer.Models.Enum;
using System.ComponentModel.DataAnnotations;
using TaskManager.Dto.Enums;

namespace TaskManager.Dto.Tasks.Request
{
    /// <summary>
    /// DTO for update exist task
    /// </summary>
    public class TaskUpdateDto
    {
        [Required(ErrorMessage = "Task id can't be blank")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Task title can't be blank")]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Task finish time can't be blank")]
        public DateTime FinishTime { get; set; }

        [Required(ErrorMessage = "Task status can't be blank")]
        public StatusTask Status { get; set; }

        [Required(ErrorMessage = "Category id can't be blank")]
        public Guid CategoryId { get; set; }

        public string? CategoryName { get; set; }

    }
}
