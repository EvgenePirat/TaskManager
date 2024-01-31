using BusinessLayer.Models.Enum;
using DataAccessLayer.Entities;

namespace BusinessLayer.Models.Tasks.Response
{

    /// <summary>
    /// Model for response task entity
    /// </summary>
    public class TaskModel
    {
        public Guid Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime FinishTime { get; set; }

        public int DaysLeft { get; set; }

        public Status Status { get; set; }

        public Category? Category { get; set; }
    }
}
