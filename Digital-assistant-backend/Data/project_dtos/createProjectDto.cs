using Digital_assistant_backend.Models;

namespace Digital_assistant_backend;

public class createProjectDto
{
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Status { get; set; }
        public required string Priority { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
         public int UserId { get; set; }
        public List<taskDto>? Tasks { get; set; }

}
