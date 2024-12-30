using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Progetta.Entities;
using Microsoft.VisualBasic;

namespace Progetta.Services
{
    public class TaskService
    {
        IDbContextFactory<ProjectContext> _contextFactory;

        public TaskService(IDbContextFactory<ProjectContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        // 1. Dodawanie zadania
        public async Task AddTaskAsync(TaskToDo task)
        {
            using ProjectContext context = _contextFactory.CreateDbContext();

            DateOnly dateOnlyNow = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            TimeOnly timeOnlyStartAt = new TimeOnly(DateTime.Now.Hour + 1, 0, 0);
            TimeOnly timeOnlyDayEnd = new TimeOnly(23, 59, 0);

            task.StartAt = new DateTime(dateOnlyNow, timeOnlyStartAt);
            task.DueDate = new DateTime(dateOnlyNow, timeOnlyDayEnd);
            task.CreatedAt = DateTime.UtcNow;
            context.TasksToDo.Add(task);
            await context.SaveChangesAsync();
        }

        // 2. Pobranie wszystkich zadań
        public async Task<List<TaskToDo>> GetAllTasksToDoAsync()
        {
            using ProjectContext context = _contextFactory.CreateDbContext();
            return await context.TasksToDo
                .Include(x => x.AssignedTo)
                .Include(x => x.Project)
                .Include(x => x.Comments)
                .OrderBy(t => t.Status)  
                .ThenByDescending(t => t.Id)  
                .ToListAsync();
            }
        // 3. Pobranie zadania po ID
        public async Task<TaskToDo> GetTaskToDoByIdAsync(int id)
        {
            using ProjectContext context = _contextFactory.CreateDbContext();
            return await context.TasksToDo
                .Include(t => t.Project)
                .Include(t => t.AssignedTo)
                .Include(t => t.Comments)
                .Include(t => t.TaskTags)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        // 4. Aktualizacja zadania
        public async Task UpdateTaskToDoAsync(TaskToDo task)
        {
            using ProjectContext context = _contextFactory.CreateDbContext();
            var existingTask = await context.TasksToDo.FindAsync(task.Id);
            if (existingTask == null)
            {
                throw new Exception("TaskToDo not found.");
            }

            existingTask.Name = task.Name;
            existingTask.Description = task.Description;
            existingTask.Status = task.Status;
            existingTask.Priority = task.Priority;
            existingTask.DueDate = task.DueDate;
            existingTask.UpdatedAt = DateTime.UtcNow;
            existingTask.ProjectId = task.ProjectId;
            //existingTask.AssignedToId = task.AssignedToId;

            if(task.AssignedTo is not null)
            {
                existingTask.AssignedTo = await context.Users.FindAsync(task.AssignedTo.Id);
            }
            else
            {
                existingTask.AssignedTo = null;
            }

            //existingTask.CreatedById = task.CreatedById;

            if(task.CreatedBy is not null)
            {
                existingTask.CreatedBy = await context.Users.FindAsync(task.CreatedBy.Id);
            }
            else
            {
                existingTask.CreatedBy = null;
            }

            if(task.Project is not null)
            {
                existingTask.Project = await context.Projects.FindAsync(task.Project.Id);
            }
            else
            {
                existingTask.Project = null;
            }

            existingTask.StartAt = task.StartAt;

            context.TasksToDo.Update(existingTask);
            await context.SaveChangesAsync();
        }

        // 5. Usuwanie zadania
        public async Task DeleteTaskToDoAsync(TaskToDo taskToDo)
        {
            using ProjectContext context = _contextFactory.CreateDbContext();
            var task = await context.TasksToDo.FindAsync(taskToDo.Id);
            if (task == null)
            {
                throw new Exception("TaskToDo not found.");
            }

            context.TasksToDo.Remove(task);
            await context.SaveChangesAsync();
        }

        // 6. Pobranie wszystkich statusów
        public async Task<List<Status>> GetStatusesAsync()
        {
            using ProjectContext context = _contextFactory.CreateDbContext();
            return await context.TasksToDo
                .Select(t => t.Status)
                .Distinct()
                .ToListAsync();
        }

        // 7. Pobranie wszystkich priorytetów
        public async Task<List<TaskPriority>> GetPrioritiesAsync()
        {
            using ProjectContext context = _contextFactory.CreateDbContext();
            return await context.TasksToDo
                .Select(t => t.Priority)
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<TaskToDo>> GetTasksByProjectIdAsync(int projectId)
        {
            using ProjectContext context = _contextFactory.CreateDbContext();
            return await context.TasksToDo
                .Where(task => task.ProjectId == projectId)
                .ToListAsync();
        }
    }
}
