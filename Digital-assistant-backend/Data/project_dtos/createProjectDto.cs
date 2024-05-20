using System.ComponentModel.DataAnnotations;
using Digital_assistant_backend.Models;

namespace Digital_assistant_backend;

public class createProjectDto
{
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Description { get; set; }
        [Required]
        public required string Status { get; set; }
        [Required]
        public required string Priority { get; set; }
        [Required]
        public DateOnly StartDate { get; set; }
        [Required]
        [DateGreaterThan("StartDate",ErrorMessage="End date should be greater then start date")]
        public DateOnly EndDate { get; set; }
        
         public int UserId { get; set; }
        public List<taskDto>? Tasks { get; set; }

}
