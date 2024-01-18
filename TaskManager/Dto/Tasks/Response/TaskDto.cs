using BusinessLayer.Enum;
using DataAccessLayer.Entities;

namespace TaskManager.Dto.Tasks.Response
{
    /// <summary>
    /// DTO for response task entity
    /// </summary>
    public class TaskDto
    {
        public Guid Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime FinishTime { get; set; }

        public Status Status { get; set; }

        public Category? Category { get; set; }
    }
}
