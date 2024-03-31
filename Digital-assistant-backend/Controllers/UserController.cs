using Digital_assistant_backend.Data;
using Digital_assistant_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Digital_assistant_backend.Controllers
{
    public class UserController : Controller
    {
        private readonly ManagementDbContext _managementDbContext;
        public UserController(ManagementDbContext managementDbContext)
        {
            _managementDbContext = managementDbContext;
            
        }

        [HttpPost]
        [Route("[controller]/signup")]
        public IActionResult SignupUser([FromBody] UserRegisterationDto NewUser)
        {
            if (NewUser == null)
            {
                return BadRequest("Invalid request body. User object is null.");
            }
            var existingUser= _managementDbContext.Users.FirstOrDefault(u=>u.Email == NewUser.Email);
            if (existingUser != null)
            {
                return BadRequest("Username or email already exists.");
            }
            string hashedPassword=BCrypt.Net.BCrypt.HashPassword(NewUser.Password);
            User user = new User 
            { 
                Name = NewUser.Name,
                Email = NewUser.Email,
                Password = hashedPassword,
                Projects=new List<Project> { },
                Dashboard=new Dashboard()
            };
            _managementDbContext.Users.Add(user);
            _managementDbContext.SaveChanges();
            return Ok("Registeration Successfull");
        }
        [HttpGet]
        [Route("[controller]/GetUsers")]
        public IActionResult GetUsers()
        {
            var users= _managementDbContext.Users.ToList();
            List<UserDto> userDtos= new List<UserDto>();
            foreach (var user in users)
            {
                var newUser = new UserDto
                {
                    Email = user.Email,
                    Name = user.Name,
                };
                userDtos.Add(newUser);
            }
            return Ok(userDtos);
        }
        [HttpGet]
        [Route("[controller]/GetById/{id:int}")]
        public IActionResult GetUserById([FromRoute] int id)
        {
            var user = _managementDbContext.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return BadRequest("No such user exist");
            }
            var newUser = new UserDto
            {
                Email = user.Email,
                Name = user.Name,
            };

            return Ok(newUser);
        }
        [HttpPost]
        [Route("[controller]/login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest("Invalid information");
            }
            var user= _managementDbContext.Users.FirstOrDefault(x=>x.Email == loginDto.Email);

            if (user == null)
            {
                return BadRequest("Invalid usrname or password");
            }
            bool isPasswordCorrect=BCrypt.Net.BCrypt.Verify(loginDto.Password,user.Password);
            if (!isPasswordCorrect)
            {
                return BadRequest("Invalid usrname or password");
            }

            return Ok(new { UserName = user.Email, UserId = user.Id });
        }
    }
}
