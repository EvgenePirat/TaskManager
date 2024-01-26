using DataAccessLayer.DbContext;
using DataAccessLayer.RepositoryContract;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Entities.Task> AddTaskAsync(Entities.Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task DeleteByIdAsync(Guid taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Entities.Task>> GetAllTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<List<Entities.Task>> GetAllTasksByCategoryIdAsync(Guid categoryId)
        {
            return await _context.Tasks.Where(temp =>  temp.CategoryId == categoryId).ToListAsync();
        }

        public async Task<Entities.Task?> GetTaskByIdAsync(Guid taskId, bool include)
        {
            if(include)
                return await _context.Tasks.Include("Category").FirstOrDefaultAsync(temp => temp.Id == taskId);
            else
                return await _context.Tasks.FirstOrDefaultAsync(temp => temp.Id == taskId);
        }

        public async Task<Entities.Task?> UpdateTaskAsync(Entities.Task task)
        {
            Entities.Task? taskFromDb = await _context.Tasks.FirstOrDefaultAsync(temp => temp.Id == task.Id);

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
