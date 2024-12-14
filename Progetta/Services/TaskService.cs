using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Progetta.Entities;

namespace Progetta.Services
{
    public class TaskService
    {
        private readonly ProjectContext _context;

        public TaskService(ProjectContext context)
        {
            _context = context;
        }

        // 1. Dodawanie zadania
        public async Task AddTaskAsync(TaskToDo task)
        {
            task.CreatedAt = DateTime.UtcNow;
            _context.TasksToDo.Add(task);
            await _context.SaveChangesAsync();
        }

        // 2. Pobranie wszystkich zadań
        public async Task<List<TaskToDo>> GetAllTasksToDoAsync()
        {
            return await _context.TasksToDo
                .OrderBy(t => t.Status)  
                .ThenByDescending(t => t.Id)  
                .ToListAsync();
            }
        // 3. Pobranie zadania po ID
        public async Task<TaskToDo> GetTaskToDoByIdAsync(int id)
        {
            return await _context.TasksToDo
                .Include(t => t.Project)
                .Include(t => t.AssignedTo)
                .Include(t => t.Comments)
                .Include(t => t.TaskTags)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        // 4. Aktualizacja zadania
        public async Task UpdateTaskToDoAsync(TaskToDo task)
        {
            var existingTask = await _context.TasksToDo.FindAsync(task.Id);
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
            existingTask.AssignedToId = task.AssignedToId;

            _context.TasksToDo.Update(existingTask);
            await _context.SaveChangesAsync();
        }

        // 5. Usuwanie zadania
        public async Task DeleteTaskToDoAsync(int id)
        {
            var task = await _context.TasksToDo.FindAsync(id);
            if (task == null)
            {
                throw new Exception("TaskToDo not found.");
            }

            _context.TasksToDo.Remove(task);
            await _context.SaveChangesAsync();
        }

        // 6. Pobranie wszystkich statusów
        public async Task<List<Status>> GetStatusesAsync()
        {
            return await _context.TasksToDo
                .Select(t => t.Status)
                .Distinct()
                .ToListAsync();
        }

        // 7. Pobranie wszystkich priorytetów
        public async Task<List<TaskPriority>> GetPrioritiesAsync()
        {
            return await _context.TasksToDo
                .Select(t => t.Priority)
                .Distinct()
                .ToListAsync();
        }
    }
}
