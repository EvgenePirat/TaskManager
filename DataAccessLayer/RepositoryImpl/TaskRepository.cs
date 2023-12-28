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

        public async Task<List<Entities.Task>> GetAllTasks(Guid categoryId)
        {
            return await _context.Tasks.Where(temp =>  temp.CategoryId == categoryId).ToListAsync();
        }
    }
}
