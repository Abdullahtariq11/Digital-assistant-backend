using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Digital_assistant_backend.Data
{
    public class UserRegisterationDto
    {
        [Required]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public required  string Email { get; set; }

       [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password should be minimum of 6 characters")]
        public required string Password { get; set; }
    }
}
