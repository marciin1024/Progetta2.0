using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Progetta.Entities;

namespace Progetta.Services
{
    public class ProjectService
    {
        private readonly IDbContextFactory<ProjectContext> _contextFactory;

        public ProjectService(IDbContextFactory<ProjectContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        // 1. Dodawanie projektu
        public async Task AddProjectAsync(Project project)
        {
            using ProjectContext context = await _contextFactory.CreateDbContextAsync();

            project.CreatedAt = DateTime.UtcNow;
            context.Projects.Add(project);
            await context.SaveChangesAsync();
        }

        // 2. Pobranie wszystkich projektów
        public async Task<List<Project>> GetAllProjectsAsync()
        {
            using ProjectContext context = await _contextFactory.CreateDbContextAsync();
            return await context.Projects
                .Include(x => x.Category)
                .OrderBy(t => t.CreatedAt)
                .ToListAsync();
        }

        // 3. Aktualizacja projektu
        public async Task UpdateProjectAsync(Project project)
        {
            using ProjectContext context = await _contextFactory.CreateDbContextAsync();
            var existingProject = await context.Projects.FindAsync(project.Id);
            if (existingProject == null)
            {
                throw new Exception("Project not found.");
            }

            existingProject.Name = project.Name;
            existingProject.Description = project.Description;
            existingProject.DueDate = project.DueDate;
            existingProject.CreatedAt = project.CreatedAt;
            existingProject.UpdatedAt = DateTime.UtcNow;
            existingProject.OwnerId = project.OwnerId;
            existingProject.CategoryId = project.CategoryId;
            existingProject.StartAt = project.StartAt;

            context.Projects.Update(existingProject);
            await context.SaveChangesAsync();
        }

        // 4. Usuwanie projektu
        public async Task DeleteProjectAsync(Project project)
        {
            using ProjectContext context = await _contextFactory.CreateDbContextAsync();
            var proj = await context.Projects.FindAsync(project.Id);
            if (proj == null)
            {
                throw new Exception("Project not found.");
            }
            context.Projects.Remove(proj);
            await context.SaveChangesAsync();
        }

        // 7. Pobranie wszystkich kategorii
        public async Task<List<Category>> GetCategoriesAsync()
        {
            using ProjectContext context = await _contextFactory.CreateDbContextAsync();
            return await context.Category
                .Include(x => x.Projects)
                .OrderBy(u => u.Name)
                .ToListAsync();
        }
    }
}
