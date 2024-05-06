using Digital_assistant_backend.Data;
using Digital_assistant_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Digital_assistant_backend.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
            
        }

        [HttpPost]
        [Route("[controller]/signup")]
        public async Task<IActionResult> SignupUser([FromBody] UserRegisterationDto NewUser)
        {
            var result=await _userService.SignupUser(NewUser);
            if(!result.Success)
            {
                return BadRequest(result.Message);
            }
            
            
            return Ok(result.Data);
        }
        [HttpGet]
        [Route("[controller]/GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var result=await _userService.GetUsers();
            return Ok(result.Data);
        }
        [HttpGet]
        [Route("[controller]/GetById/{id:int}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            var result= await _userService.GetUserById(id);
            if(!result.Success)
            {
                return BadRequest(result.Message);
            }


            return Ok(result.Data);
        }
        [HttpPost]
        [Route("[controller]/login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result=await _userService.Login(loginDto);
            if(!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }
    }
}
