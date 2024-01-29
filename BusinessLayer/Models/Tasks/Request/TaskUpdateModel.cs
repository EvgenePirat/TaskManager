using BusinessLayer.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.Tasks.Request
{
    /// <summary>
    /// Model for update exist task
    /// </summary>
    public class TaskUpdateModel
    {
        public Guid Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime FinishTime { get; set; }

        public Status Status { get; set; }

        public Guid CategoryId { get; set; }

        public string? CategoryName { get; set; }

    }
}
