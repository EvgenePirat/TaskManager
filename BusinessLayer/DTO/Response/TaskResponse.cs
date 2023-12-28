using BusinessLayer.Enum;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTO.Response
{
    /// <summary>
    /// DTO for response task entity
    /// </summary>
    public class TaskResponse
    {
        public Guid Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime FinishTime { get; set; }

        public Status Status { get; set; }

        public Category? Category { get; set; }
    }
}
