using Microsoft.AspNetCore.Identity;
namespace Digital_assistant_backend.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public ICollection<Project> Projects { get; set; }

    }
}