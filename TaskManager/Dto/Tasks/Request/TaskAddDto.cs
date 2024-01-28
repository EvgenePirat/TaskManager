using BusinessLayer.Enum;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Dto.Tasks.Request
{
    /// <summary>
    /// DTO for add model task
    /// </summary>
    public class TaskAddDto
    {
        [Required(ErrorMessage = "Task title can't be blank")]
        [Remote(action: "CheckExistNameTask", controller: "RemoteValidation", ErrorMessage = "Task with title already exist")]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Task finish time can't be blank")]
        public DateTime FinishTime { get; set; }

        [Required(ErrorMessage = "Task status can't be blank")]
        public Status Status { get; set; }

        [Required(ErrorMessage = "Category id can't be blank")]
        public Guid CategoryId { get; set; }
    }
}
