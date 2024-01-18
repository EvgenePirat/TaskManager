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
        /// Method for get task with id
        /// </summary>
        /// <param name="taskId">task id for dound task in system</param>
        /// <returns>returned task response or throw argumetException()</returns>
        public Task<TaskModel> GetTaskWithIdAsync(Guid taskId);

        /// <summary>
        /// Method for add businnes logic before delete task
        /// </summary>
        /// <param name="taskId">guid id for check exist or not</param>
        public Task DeleteWithIdAsync(Guid taskId);

        /// <summary>
        /// Method for add businnes logic before update task
        /// </summary>
        /// <param name="taskUpdate">task with data for update</param>
        /// <returns>returned already updated task with new data</returns>
        public Task<TaskModel> UpdateTaskAsync(TaskUpdateModel taskUpdate);
    }
}
