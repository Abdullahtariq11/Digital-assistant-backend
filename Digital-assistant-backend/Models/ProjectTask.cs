using System.ComponentModel.DataAnnotations;

namespace Digital_assistant_backend.Models
{
    public class ProjectTask
    {
        [Key]
        public int Id { get; set; }
        public DateOnly startDate { get; set; }
        public DateOnly endDate { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public DateOnly dueDate { get; set; }

        // Foreign key to represent the relationship
        public int ProjectId { get; set; }
        public Project Project { get; set; }

    }
}
