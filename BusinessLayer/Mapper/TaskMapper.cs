using Task = DataAccessLayer.Entities.Task;
using BusinessLayer.Enum;
using BusinessLayer.DTO.TaskDto.Request;
using BusinessLayer.DTO.TaskDto.Response;

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
        public static Task TaskAddRequestToTask(TaskAddModel taskAddRequest)
        {
            return new Task() { CategoryId = taskAddRequest.CategoryId, Title = taskAddRequest.Title, Description = taskAddRequest.Description, FinishTime = taskAddRequest.FinishTime, Status = taskAddRequest.Status.ToString() };
        }

        /// <summary>
        /// Method with logic for change from task class to TaskResponse class
        /// </summary>
        /// <param name="task">task has data for TaskResponse</param>
        /// <returns>returned TaskResponse with data from task</returns>
        public static TaskModel TaskToTaskResponse(Task task)
        {
            return new TaskModel() { Category = task.Category, Description = task.Description, Id = task.Id, FinishTime = task.FinishTime, Title = task.Title, Status = System.Enum.Parse<Status>(task.Status) };
        }

        /// <summary>
        /// Method with logic for change from taskUpdateRequest class to task class for update in bd
        /// </summary>
        /// <param name="taskUpdateRequest">taskUpdateRequest has data for task</param>
        /// <returns>returned task with data from taskUpdateRequest</returns>
        public static Task TaskUpdateRequestToTask(TaskUpdateModel taskUpdateRequest)
        {
            return new Task() { Id = taskUpdateRequest.Id, Title = taskUpdateRequest.Title, Description = taskUpdateRequest.Description, FinishTime = taskUpdateRequest.FinishTime, Status = taskUpdateRequest?.Status.ToString(), CategoryId = taskUpdateRequest.CategoryId };
        }

        /// <summary>
        /// Method with logic for change from taskResponse class to taskUpdateRequest class
        /// </summary>
        /// <param name="taskResponse"></param>
        /// <returns></returns>
        public static TaskUpdateModel TaskResponseToTaskUpdateRequest(TaskModel taskResponse)
        {
            return new TaskUpdateModel() {  Id = taskResponse.Id, Title = taskResponse.Title, Description = taskResponse.Description, FinishTime = taskResponse.FinishTime, CategoryId = taskResponse.Category.Id, CategoryName = taskResponse.Category.Name, Status = taskResponse.Status };
        }
    }
}
