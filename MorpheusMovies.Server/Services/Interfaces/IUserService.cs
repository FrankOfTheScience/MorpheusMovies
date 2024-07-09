using MorpheusMovies.Server.EF.Model;

namespace MorpheusMovies.Server.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<ApplicationUser>> GetApplicationUsersAsync();
    Task<ApplicationUser> GetApplicationUsersByIdAsync(int id);
    Task<ApplicationUser> GetApplicationUserByEmailAsync(string email);
    Task EditUserProfileAsync(ApplicationUser editedUser);
    Task DeleteUserProfileAsync(string email);
    Task SignUp(ApplicationUser newUser);
    Task SignIn(string email, string password);
    Task ChangePasswordAsync(string email, string password);
}
