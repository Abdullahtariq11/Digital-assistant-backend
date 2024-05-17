using System.ComponentModel.DataAnnotations;

namespace Digital_assistant_backend.Data
{
    public class LoginDto
    {   
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string  Password { get; set; }
    }
}
