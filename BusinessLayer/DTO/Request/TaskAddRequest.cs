using BusinessLayer.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTO.Request
{
    /// <summary>
    /// DTO for add model task
    /// </summary>
    public class TaskAddRequest
    {
        [Required]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime FinishTime { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
    }
}
