using Progetta.Enums;
using System.ComponentModel.DataAnnotations;

namespace Progetta.Entities
{
    public class User
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public LabelId LabelId { get; set; } = LabelId.Light;

        public string FullName => $"{FirstName} {LastName}";

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Project> OwnedProjects { get; set; }
        public ICollection<UserProject> CollaboratedProjects { get; set; }
        public ICollection<TaskToDo> AssignedTasks { get; set; }
        public ICollection<TaskToDo> CreatedTasks { get; set; }

        public override string ToString()
        {
            return FullName;
        }
    }

    public enum UserRole
    {
        Admin,
        User
    }
}
