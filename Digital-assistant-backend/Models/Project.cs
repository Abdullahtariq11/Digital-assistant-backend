using System.ComponentModel.DataAnnotations;

namespace Digital_assistant_backend.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        // Foreign key to represent the relationship
        public int UserId { get; set; }
        public User User { get; set; }

        // Navigation property for one-to-many relationship
        public List<ProjectTask> Tasks { get; set; }
    }
}
