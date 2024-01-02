﻿using System;
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
    }
}