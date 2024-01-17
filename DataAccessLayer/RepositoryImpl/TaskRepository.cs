using DataAccessLayer.RepositoryContract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.RepositoryImpl
{
    /// <summary>
    /// Implementation logic for task repository contract
    /// </summary>
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task<Entities.Task> AddTask(Entities.Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task DeleteById(Guid taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Entities.Task>> GetAllTasks(Guid categoryId)
        {
            return await _context.Tasks.Where(temp =>  temp.CategoryId == categoryId).ToListAsync();
        }

        public async Task<Entities.Task?> GetTaskById(Guid taskId)
        {
            return await _context.Tasks.Include("Category").FirstOrDefaultAsync(temp => temp.Id == taskId);
        }

        public async Task<Entities.Task?> UpdateTask(Entities.Task task)
        {
            Entities.Task? taskFromDb = await _context.Tasks.FirstOrDefaultAsync(temp => temp.Id != task.Id);

            if(taskFromDb != null)
            {
                taskFromDb.Title = task.Title;
                taskFromDb.Description = task.Description;
                taskFromDb.Status = task.Status;
                taskFromDb.FinishTime = task.FinishTime;
                taskFromDb.CategoryId = task.CategoryId;

                _context.SaveChanges();
            }

            return taskFromDb;
        }
    }
}
