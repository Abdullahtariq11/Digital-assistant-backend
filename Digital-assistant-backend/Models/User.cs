using System.ComponentModel.DataAnnotations;

namespace Digital_assistant_backend.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

        public List<Project>? Projects { get; set; }
        public Dashboard ?Dashboard { get; set; }
    }
}
