namespace Digital_assistant_backend;
using Digital_assistant_backend.Data;

public interface IUserService
{
    public Task<Service<UserDto>> SignupUser(UserRegisterationDto NewUser);
    public Task<Service<UserDto>> GetUserById(string id);
    public Task<Service<List<UserDto>>> GetUsers();
    public Task<Service<UserDto>> Login(LoginDto loginDto);

}
