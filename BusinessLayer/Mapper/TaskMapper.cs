using BusinessLayer.DTO.Request;
using BusinessLayer.DTO.Response;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = DataAccessLayer.Entities.Task;
using BusinessLayer.Enum;

namespace BusinessLayer.Mapper
{
    /// <summary>
    /// Class mapper allows change class on other class with save data for task entity
    /// </summary>
    public static class TaskMapper
    {
        /// <summary>
        /// Method with logic for change from taskAddRequest class to task class for save in bd
        /// </summary>
        /// <param name="taskAddRequest">taskAddRequest has data for task</param>
        /// <returns>returned task with data from taskAddRequest</returns>
        public static Task TaskAddRequestToTask(TaskAddRequest taskAddRequest)
        {
            return new Task() { CategoryId = taskAddRequest.CategoryId, Title = taskAddRequest.Title, Description = taskAddRequest.Description, FinishTime = taskAddRequest.FinishTime, Status = taskAddRequest.Status.ToString() };
        }

        /// <summary>
        /// Method with logic for change from task class to TaskResponse class
        /// </summary>
        /// <param name="task">task has data for TaskResponse</param>
        /// <returns>returned TaskResponse with data from task</returns>
        public static TaskResponse TaskToTaskResponse(Task task)
        {
            return new TaskResponse() { Category = task.Category, Description = task.Description, Id = task.Id, FinishTime = task.FinishTime, Title = task.Title, Status = System.Enum.Parse<Status>(task.Status) };
        }

    }
}
