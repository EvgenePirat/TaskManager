using AutoMapper;
using BusinessLayer.Enum;
using DataAccessLayer.RepositoryContract;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
namespace BusinessLayer.BackgroundServices
{
    /// <summary>
    /// Class service for work in background tasks for check all task on finish time
    /// </summary>
    public class TaskSchedulerService : BackgroundService
    {
        private readonly ILogger<TaskSchedulerService> _logger;
        private readonly ITaskRepository _taskRepository;
        private readonly TimeSpan _interval = TimeSpan.FromHours(12);
        private readonly IMapper _mapper;

        public TaskSchedulerService(ILogger<TaskSchedulerService> logger, ITaskRepository taskRepository, IMapper mapper)
        {
            _logger = logger;
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Method in service start work every 12 hours
        /// </summary>
        /// <param name="stoppingToken">cancel token if application is finished</param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("{service}.{method} - start, check all task on time implementation!", nameof(TaskSchedulerService), nameof(ExecuteAsync));

            while(!stoppingToken.IsCancellationRequested)
            {
                await CheckTaskStatus();

                _logger.LogInformation("{service}.{method} - finish, check all task on finish time!", nameof(TaskSchedulerService), nameof(ExecuteAsync));

                await Task.Delay(_interval, stoppingToken);
            }
        }

        /// <summary>
        /// Method for check and change status for overdue task
        /// </summary>
        /// <returns></returns>
        private async Task CheckTaskStatus()
        {
            var allTasks = await _taskRepository.GetAllTasksAsync();
            var overdueTasks = allTasks.Where(temp => temp.FinishTime < DateTime.Now);

            foreach (var task in overdueTasks)
            {
                task.Status = Status.Overdue.ToString();
                await _taskRepository.UpdateTaskAsync(task);
            }
        }
    }
}
