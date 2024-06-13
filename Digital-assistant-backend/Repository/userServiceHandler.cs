using Digital_assistant_backend.Data;
using Digital_assistant_backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Digital_assistant_backend;

public class userServiceHandler : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public userServiceHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }


    
  public async Task<Service<UserDto>> GetUserById(string id)
    {
        var userNew= await _userManager.FindByIdAsync(id);
        if(userNew==null){
            return Service<UserDto>.failure("incorrect data");
        }
        var user=new UserDto
        {
            id=userNew.Id,
            Email=userNew.Email,
            Name=userNew.Name,
        };
        return Service<UserDto>.success(user);;
       
    }

    public async Task<Service<List<UserDto>>> GetUsers()
    {
        var users= await  _userManager.Users.ToListAsync();
        List<UserDto> userDtos= new List<UserDto>();
        foreach(var user in users){
            var newUser = new UserDto
            {
                id=user.Id,
                Email=user.Email,
                Name=user.Name,
            };
            userDtos.Add(newUser);
        }
        return Service<List<UserDto>>.success(userDtos); 
    }

    public  async Task<Service<UserDto>> Login(LoginDto loginDto)
    {
       if (loginDto == null)
        {
            return Service<UserDto>.failure("Invalid data");
        }

        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
        {
            return Service<UserDto>.failure("Incorrect password or username");
        }

        var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
        if (!result.Succeeded)
        {
            return Service<UserDto>.failure("Incorrect password or username");
        }

        var userDto = new UserDto
        {
            id = user.Id,
            Email = user.Email,
            Name = user.Name
        };

        return Service<UserDto>.success(userDto);
    }

    public async Task<Service<UserDto>> SignupUser(UserRegisterationDto newUser)
    {
        if (newUser == null)
        {
            return Service<UserDto>.failure("Invalid data");
        }

        var existingUser = await _userManager.FindByEmailAsync(newUser.Email);
        if (existingUser != null)
        {
            return Service<UserDto>.failure("User already exists");
        }

        var user = new ApplicationUser
        {
            Name = newUser.Name,
            Email = newUser.Email,
            UserName = newUser.Email
        };

        var result = await _userManager.CreateAsync(user, newUser.Password);
        if (!result.Succeeded)
        {
            return Service<UserDto>.failure(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        var userDto = new UserDto
        {
            id = user.Id,
            Email = user.Email,
            Name = user.Name
        };

        return Service<UserDto>.success(userDto);
        
    }


}

