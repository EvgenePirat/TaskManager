using BusinessLayer.DTO.Request;
using BusinessLayer.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Task<TaskResponse> AddNewTask(TaskAddRequest taskAddRequest);

        /// <summary>
        /// method for get all tasks for categories
        /// </summary>
        /// <param name="categoryId">category id for search need tasks</param>
        /// <returns>returned all tasks for category</returns>
        public Task<List<TaskResponse>> GetAllTaskForCategories(Guid categoryId);
    }
}
