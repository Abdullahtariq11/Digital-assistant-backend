using System.ComponentModel.DataAnnotations;

namespace Digital_assistant_backend.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Status { get; set; }
        public required string Priority { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        // Foreign key to represent the relationship
        public int UserId { get; set; }
        public required User User { get; set; }

        // Navigation property for one-to-many relationship
        public List<ProjectTask>? Tasks { get; set; }


    }
}
