using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Progetta.Entities
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {

        }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TaskToDo> TasksToDo { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TaskTag> TaskTags { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskTag>()
               .HasKey(tt => new { tt.TaskId, tt.TagId });

            modelBuilder.Entity<UserProject>()
                .HasKey(up => new { up.UsernameId, up.ProjectId });

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
               .HasOne(c => c.Task)
               .WithMany(t => t.Comments)
               .HasForeignKey(c => c.TaskId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserProject>()
                .HasOne(up => up.Username)
                .WithMany()
                .HasForeignKey(up => up.UsernameId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserProject>()
                .HasOne(up => up.Project)
                .WithMany(p => p.UserProjects)
                .HasForeignKey(up => up.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

           modelBuilder.Entity<TaskToDo>()
               .HasOne(t => t.CreatedBy)
               .WithMany(u => u.CreatedTasks) 
               .HasForeignKey(t => t.CreatedById)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasData(new User { Id = 1, FirstName = "Marcin", LastName = "Nowak", Email = "marcin@gmail.com", Password = "1234", Role = UserRole.Admin },
                new User { Id = 2, FirstName = "Sebastian", LastName = "Kowalski", Email = "sebastian@gmail.com", Password = "1234", Role = UserRole.User },
                new User { Id = 3, FirstName = "Leszek", LastName = "Malinowski", Email = "leszek@gmail.com", Password = "1234", Role = UserRole.User }
                );

            modelBuilder.Entity<Project>()
                .HasData(new Project
                {
                    Id = 1,
                    Name = "Pierwszy projekt",
                    Description = "To jest opis projektu",
                    OwnerId = 1,
                },
                new Project
                {
                    Id = 2,
                    Name = "Drugi projekt",
                    Description = "To jest opis projektu",
                    OwnerId = 2,
                });



            modelBuilder.Entity<TaskToDo>().
                HasData(new TaskToDo
                {
                    Id = 1,
                    Name = "Pierwsze zadanie",
                    ProjectId = 1,
                },
                new TaskToDo
                {
                    Id = 2,
                    Name = "Drugie zadanie",
                    ProjectId = 2,
                    Priority = TaskPriority.High,
                    Status = Status.ToDo,
                    DueDate = DateTime.Now,
                    AssignedToId = 2,
                },
                new TaskToDo
                {
                    Id = 3,
                    Name = "Trzecie zadanie",
                    ProjectId = 2,
                    Priority = TaskPriority.Low,
                    Status = Status.Done,
                    DueDate = DateTime.Now,
                },
                new TaskToDo
                {
                    Id = 4,
                    Name = "Czwarte zadanie",
                    ProjectId = 1,
                    Priority = TaskPriority.High,
                    Description = "To jest opis zadania",
                    AssignedToId = 1,
                });
            modelBuilder.Entity<Comment>().
                HasData(new Comment
                {
                    Id = 1,
                    Message = "Lubię to!",
                    UserId = 1,
                    TaskId = 1,
                },
                new Comment
                {
                    Id = 2,
                    Message = "Super!",
                    UserId = 1,
                    TaskId = 2,
                },
                new Comment
                {
                    Id = 3,
                    Message = "Wow!",
                    UserId = 2,
                    TaskId = 1,
                });
            modelBuilder.Entity<Tag>().
                HasData(new Tag
                {
                    Id = 1,
                    Name = "Web"
                });
            modelBuilder.Entity<TaskTag>().
                HasData(new TaskTag
                {
                    TaskId = 1,
                    TagId = 1,
                });
            modelBuilder.Entity<UserProject>().
                HasData(new UserProject
                {
                    UsernameId = 1,
                    ProjectId = 2,
                    Role = UserProjectRole.Collaborator,

                });
        }
    }
}
