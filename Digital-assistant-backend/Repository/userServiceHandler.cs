using Digital_assistant_backend.Data;
using Digital_assistant_backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Digital_assistant_backend;

public class userServiceHandler : IUserService
{
    private readonly ManagementDbContext _dbContext;

   public userServiceHandler(ManagementDbContext dbContext)
   {
        _dbContext = dbContext;
   }


    
    public async Task<Service<UserDto>> GetUserById(int id)
    {
        var userNew= await _dbContext.Users.FirstOrDefaultAsync(x=>x.Id==id);
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
        var users= await  _dbContext.Users.ToListAsync();
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
        if(loginDto==null)
        {
            return Service<UserDto>.failure("incorrect data");
        }
        var user = await _dbContext.Users.FirstOrDefaultAsync(x=>x.Email==loginDto.Email);

        if(user==null)
        {
            return Service<UserDto>.failure("incorrect password or username");
        }
        bool isPasswordCorrect=BCrypt.Net.BCrypt.Verify(loginDto.Password,user.Password);
        if(!isPasswordCorrect)
        {
            return Service<UserDto>.failure("incorrect password or username");
        }
        var newUser= new UserDto
        {
            id=user.Id,
            Email=user.Email,
            Name=user.Name,
        };
        return Service<UserDto>.success(newUser);
    }

    public async Task<Service<UserDto>> SignupUser(UserRegisterationDto NewUser)
    {
        if (NewUser==null)
        {
            return Service<UserDto>.failure("incorrect data");
        }
        var existingUser=await _dbContext.Users.FirstOrDefaultAsync(x=>x.Email==NewUser.Email);
        if(existingUser!=null)
        {
            return Service<UserDto>.failure("user already exists");
        }
        string hashedPassword=BCrypt.Net.BCrypt.HashPassword(NewUser.Password);

        var user= new User 
        {
            Name=NewUser.Name,
            Email=NewUser.Email, 
            Password=hashedPassword,
            Projects=new List<Project>{},
            Dashboard=new Dashboard(),
        };
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        var userDt= new UserDto
        {
            id=user.Id,
            Email=user.Email,
            Name=user.Name,
        };
        return Service<UserDto>.success(userDt);

        
    }


}

