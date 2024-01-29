using BusinessLayer.Enum;
using BusinessLayer.Models.Tasks.Request;
using BusinessLayer.Models.Tasks.Response;

namespace BusinessLayer.ServiceContract
{
    /// <summary>
    /// Rerpesents business logic for manipoulating task entity
    /// </summary>
    public interface ITaskService
    {
        /// <summary>
        /// Method for added logic before save in bd
        /// </summary>
        /// <param name="taskAddRequest">data abount task from client</param>
        /// <returns>returned task with id after save</returns>
        public Task<TaskModel> AddNewTaskAsync(TaskAddModel taskAddRequest);

        /// <summary>
        /// method for get all tasks for categories
        /// </summary>
        /// <param name="categoryId">category id for search need tasks</param>
        /// <returns>returned all tasks for category</returns>
        public Task<List<TaskModel>> GetAllTaskForCategoriesAsync(Guid categoryId);

        /// <summary>
        /// Method for get task by id
        /// </summary>
        /// <param name="taskId">task id for found task in system</param>
        /// <param name="include">bool type for include category object in task or not include</param>
        /// <returns>returned task response or throw argumetException()</returns>
        public Task<TaskModel> GetTaskByIdAsync(Guid taskId,  bool include);

        /// <summary>
        /// Method for get task by title
        /// </summary>
        /// <param name="titleTask">task title for found task in system  for user</param>
        /// <param name="loginUser">string login for find all tasks for user</param>
        /// <returns>returned task response or throw exception</returns>
        public Task<TaskModel?> GetTaskByTitleAsync(string titleTask, string? loginUser);

        /// <summary>
        /// Method for add businnes logic before delete task
        /// </summary>
        /// <param name="taskId">guid id for check exist or not</param>
        public Task DeleteByIdAsync(Guid taskId);

        /// <summary>
        /// Method for add businnes logic before update task
        /// </summary>
        /// <param name="taskUpdate">task with data for update</param>
        /// <returns>returned already updated task with new data</returns>
        public Task<TaskModel> UpdateTaskAsync(TaskUpdateModel taskUpdate);

        /// <summary>
        /// Method for add businnes logic before change status for task
        /// </summary>
        /// <param name="newStatus">new status for set in task</param>
        /// <param name="taskId">task id for search task in bd</param>
        /// <returns>returned task with new status</returns>
        public Task<TaskModel> ChangeStatusForTask(Status newStatus,  Guid taskId);
    }
}
