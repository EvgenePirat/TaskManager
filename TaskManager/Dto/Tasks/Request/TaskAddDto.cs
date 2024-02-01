using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TaskManager.Dto.Enums;

namespace TaskManager.Dto.Tasks.Request
{
    /// <summary>
    /// DTO for add model task
    /// </summary>
    public class TaskAddDto
    {
        [Required(ErrorMessage = "Task title can't be blank")]
        [Remote(action: "CheckExistUserName", controller: "RemoteValidation", ErrorMessage = "Task with title already exist")]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Task finish time can't be blank")]
        public DateTime FinishTime { get; set; }

        [Required(ErrorMessage = "Task status can't be blank")]
        public StatusTask Status { get; set; }

        [Required(ErrorMessage = "Category id can't be blank")]
        public Guid CategoryId { get; set; }
    }
}
