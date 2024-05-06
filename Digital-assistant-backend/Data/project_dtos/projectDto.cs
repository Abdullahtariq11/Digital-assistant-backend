namespace Digital_assistant_backend;

public class projectDto
{
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Status { get; set; }
        public required string Priority { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
}
