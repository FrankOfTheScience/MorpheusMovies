using MorpheusMovies.Server.DTOs;
using MorpheusMovies.Server.EF.Model;

namespace MorpheusMovies.Server.Services.Interfaces;

public interface IUserService
{
    Task<ResponseBase> GetApplicationUsersAsync();
    Task<ResponseBase> GetApplicationUsersByIdAsync(int id);

    Task<ResponseBase> GetApplicationUserByEmailAsync(string email);
    Task<ResponseBase> EditUserProfileAsync(ApplicationUser editedUser);
    Task<ResponseBase> DeleteUserProfileAsync(string email);
    Task<ResponseBase> SignUp(ApplicationUser newUser);
    Task<ResponseBase> SignIn(string email, string password);
    Task<ResponseBase> ChangePasswordAsync(string email, string password);
}
