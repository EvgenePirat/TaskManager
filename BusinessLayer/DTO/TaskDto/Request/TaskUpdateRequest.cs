using BusinessLayer.Enum;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTO.TaskDto.Request
{
    /// <summary>
    /// DTO for update exist task
    /// </summary>
    public class TaskUpdateRequest
    {
        public Guid Id { get; set; }

        [Required]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime FinishTime { get; set; }

        [Required]
        public Status Status { get; set; }

        public Guid CategoryId { get; set; }

    }
}
}
