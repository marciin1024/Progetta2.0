﻿using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Progetta.Entities;

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
            task.CreatedAt = DateTime.UtcNow;
            context.TasksToDo.Add(task);
            await context.SaveChangesAsync();
        }

        // 2. Pobranie wszystkich zadań
        public async Task<List<TaskToDo>> GetAllTasksToDoAsync()
        {
            using ProjectContext context = _contextFactory.CreateDbContext();
            return await context.TasksToDo
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
            existingTask.AssignedToId = task.AssignedToId;
            existingTask.CreatedById = task.CreatedById;
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
    }
}
