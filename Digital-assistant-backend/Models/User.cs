using System.ComponentModel.DataAnnotations;

namespace Digital_assistant_backend.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<Project> Projects { get; set; }
        public Dashboard Dashboard { get; set; }
    }
}
