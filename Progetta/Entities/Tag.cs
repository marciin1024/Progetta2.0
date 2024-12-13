using System.ComponentModel.DataAnnotations;

namespace Progetta.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<TaskTag> TaskTags { get; set; }
    }
}
