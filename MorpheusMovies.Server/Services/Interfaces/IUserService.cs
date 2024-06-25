using MorpheusMovies.Server.EF.Model;

namespace MorpheusMovies.Server.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<ApplicationUser>> GetApplicationsAsync();
    Task<ApplicationUser> GetApplicationUserByEmailAsync(string email);
    Task<ApplicationUser> EditUserProfileAsync(ApplicationUser editedUser);
    Task<bool> DeleteUserProfileAsync(string email);
    Task<bool> SignUp(ApplicationUser newUser);
    Task<bool> SignIn(string email, string password);
    Task<bool> ChangePasswordAsync(string email, string password);
}
