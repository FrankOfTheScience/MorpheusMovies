using MorpheusMovies.Server.EF.Model;
using MorpheusMovies.Server.Repository.Interfaces;
using MorpheusMovies.Server.Services.Interfaces;

namespace MorpheusMovies.Server.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
        => _userRepository = userRepository;

    public Task<bool> ChangePasswordAsync(string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUserProfileAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationUser> EditUserProfileAsync(ApplicationUser editedUser)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ApplicationUser>> GetApplicationsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationUser> GetApplicationUserByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SignIn(string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SignUp(ApplicationUser newUser)
    {
        throw new NotImplementedException();
    }
}
