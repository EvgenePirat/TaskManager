using BusinessLayer.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.Tasks.Request
{
    /// <summary>
    /// Model for add model task
    /// </summary>
    public class TaskAddModel
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime FinishTime { get; set; }

        public Status Status { get; set; }

        public Guid CategoryId { get; set; }
    }
}
