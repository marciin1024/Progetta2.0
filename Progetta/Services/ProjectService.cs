using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Progetta.Entities;

namespace Progetta.Services
{
    public class ProjectService
    {
        private readonly ProjectContext _context;

        public ProjectService(ProjectContext context)
        {
            _context = context;
        }

        // 1. Dodawanie projektu
        public async Task AddProjectAsync(Project project)
        {
            project.CreatedAt = DateTime.UtcNow;
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
        }

        // 2. Pobranie wszystkich projektów
        public async Task<List<Project>> GetAllProjectsAsync()
        {
            return await _context.Projects
                .OrderBy(t => t.CreatedAt)
                .ToListAsync();
        }

        // 3. Aktualizacja projektu
        public async Task UpdateProjectAsync(Project project)
        {
            var existingProject = await _context.Projects.FindAsync(project.Id);
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

            _context.Projects.Update(existingProject);
            await _context.SaveChangesAsync();
        }

        // 4. Usuwanie projektu
        public async Task DeleteProjectAsync(Project project)
        {
            var proj = await _context.Projects.FindAsync(project.Id);
            if (proj == null)
            {
                throw new Exception("Project not found.");
            }
            _context.Projects.Remove(proj);
            await _context.SaveChangesAsync();
        }

        // 7. Pobranie wszystkich kategorii
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Category
                .Include(x => x.Projects)
                .OrderBy(u => u.Name)
                .ToListAsync();
        }
    }
}
