using System.ComponentModel.DataAnnotations;

namespace Digital_assistant_backend.Models
{
    public class ProjectTask
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Status { get; set; }

        // Foreign key to represent the relationship
        public int ProjectId { get; set; }
        public Project? Project { get; set; }

    }
}
