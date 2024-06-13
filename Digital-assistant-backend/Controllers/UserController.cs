using Digital_assistant_backend.CustomActionFilters;
using Digital_assistant_backend.Data;
using Digital_assistant_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Digital_assistant_backend.Controllers
{
    [ApiController]
    
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;

        }

        [HttpPost]
        [ValidateModel]
        [Route("[controller]/signup")]
        public async Task<IActionResult> SignupUser([FromBody] UserRegisterationDto NewUser)
        {
          
            {
                var result = await _userService.SignupUser(NewUser);
                if (!result.Success)
                {
                    return BadRequest(result.Message);
                }
                return Ok(result.Data);
            }

        }

        [HttpGet]
        [Route("[controller]/GetUsers ")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userService.GetUsers();
            return Ok(result.Data);
        }
        [HttpGet]
        [Route("[controller]/GetById/{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            var result = await _userService.GetUserById(id);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }


            return Ok(result.Data);
        }


        [HttpPost]
        [ValidateModel]
        [Route("[controller]/login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _userService.Login(loginDto);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }
    }
}
