using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.RepositoryContract
{
    /// <summary>
    /// Perpesents data access logic for managing Task entity
    /// </summary>
    public interface ITaskRepository
    {
        /// <summary>
        /// Method for save new task in bd
        /// </summary>
        /// <param name="task">data task from user</param>
        /// <returns>returned task with id after save</returns>
        public Task<Entities.Task> AddTask(Entities.Task task);

        /// <summary>
        /// Method for get all tasks for categories
        /// </summary>
        /// <param name="categoryId">category id for find need tasks</param>
        /// <returns>returned list tasks for categories</returns>
        public Task<List<Entities.Task>> GetAllTasks(Guid categoryId);  

        /// <summary>
        /// Method for get task with id
        /// </summary>
        /// <param name="taskId">task if for search task in tasks</param>
        /// <returns>returned task if find or null if not exist</returns>
        public Task<Entities.Task?> GetTaskById(Guid taskId);

        /// <summary>
        /// Method for delete task with id
        /// </summary>
        /// <param name="taskId">guid id for delete task</param>
        public Task DeleteById(Guid taskId);

        /// <summary>
        /// Method for update already exist task
        /// </summary>
        /// <param name="task">task for update</param>
        /// <returns>returned task with updated data</returns>
        public Task<Entities.Task?> UpdateTask(Entities.Task task);
    }
}
