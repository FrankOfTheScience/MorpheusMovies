using BCrypt.Net;
using MorpheusMovies.Server.DTOs;
using MorpheusMovies.Server.EF.Model;
using MorpheusMovies.Server.Repository.Interfaces;
using MorpheusMovies.Server.Services.Interfaces;
using MorpheusMovies.Server.Utilities;

namespace MorpheusMovies.Server.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
        => _userRepository = userRepository;

    public async Task<ResponseBase> ChangePasswordAsync(string email, string newPassword)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(newPassword))
                return new KoResponse(new ErrorResponseObject(string.Format(MorpheusMoviesConstants.ResponseConstants.PARAMETER_NOT_DEFINED, $"{nameof(email)}, {nameof(newPassword)}")));

            var user = await _userRepository.GetByNameAsync(email);
            if (user == null)
                return new KoResponse(new ErrorResponseObject(string.Format(MorpheusMoviesConstants.ResponseConstants.PARAMETER_NOT_DEFINED, $"{nameof(email)}, {nameof(newPassword)}")));

            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _userRepository.UpdateAsync(user);

            return new OkResponse<GeneralOkResponse>(new GeneralOkResponse(string.Format(MorpheusMoviesConstants.ResponseConstants.PASSWORD_UPDATED, email)));
        }
        catch (KeyNotFoundException e)
        {
            Console.WriteLine($"Entity not found in {nameof(ChangePasswordAsync)}: {e.Message}");
            return new KoResponse(new ErrorResponseObject(e.Message));
        }
        catch (SaltParseException e)
        {
            Console.WriteLine($"Error hashing password {nameof(ChangePasswordAsync)}: {e.Message}");
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error accessing data in {nameof(ChangePasswordAsync)}: {e.Message}");
            throw;
        }
    }

    public async Task<ResponseBase> DeleteUserProfileAsync(string email)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(email))
                return new KoResponse(new ErrorResponseObject(string.Format(MorpheusMoviesConstants.ResponseConstants.PARAMETER_NOT_DEFINED, nameof(email))));

            var user = await _userRepository.GetByNameAsync(email);
            if (user is null)
                return new KoResponse(new ErrorResponseObject(string.Format(MorpheusMoviesConstants.ResponseConstants.ENTITY_NOT_FOUND_FOR_THE_OPERATION, nameof(ApplicationUser), MorpheusMoviesConstants.CRUD_DELETE)));

            await _userRepository.DeleteAsync(user.UserId);
            return new OkResponse<GeneralOkResponse>(new GeneralOkResponse(string.Format(MorpheusMoviesConstants.ResponseConstants.ENTITY_DELETED, nameof(ApplicationUser))));
        }
        catch (KeyNotFoundException e)
        {
            Console.WriteLine($"Entity not found in {nameof(DeleteUserProfileAsync)}: {e.Message}");
            return new KoResponse(new ErrorResponseObject(e.Message));
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error accessing data in {nameof(DeleteUserProfileAsync)}: {e.Message}");
            throw;
        }
    }

    public async Task<ResponseBase> EditUserProfileAsync(ApplicationUser editedUser)
    {
        try
        {
            if (editedUser is null)
                return new KoResponse(new ErrorResponseObject(string.Format(MorpheusMoviesConstants.ResponseConstants.PARAMETER_NOT_DEFINED, nameof(ApplicationUser))));

            await _userRepository.UpdateAsync(editedUser);
            return new OkResponse<GeneralOkResponse>(new GeneralOkResponse(string.Format(MorpheusMoviesConstants.ResponseConstants.ENTITY_UPDATED, nameof(ApplicationUser))));
        }
        catch (KeyNotFoundException e)
        {
            Console.WriteLine($"Entity not found in {nameof(EditUserProfileAsync)}: {e.Message}");
            return new KoResponse(new ErrorResponseObject(e.Message));
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error accessing data in {nameof(EditUserProfileAsync)}: {e.Message}");
            throw;
        }
    }

    public async Task<ResponseBase> GetApplicationUsersAsync()
    {
        try
        {
            var records = await _userRepository.GetAllAsync();
            return new OkResponse<IEnumerable<ApplicationUser>>(records);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error accessing data in {nameof(GetApplicationUsersAsync)}: {e.Message}");
            throw;
        }
    }

    public async Task<ResponseBase> GetApplicationUserByEmailAsync(string email)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(email))
                return new KoResponse(new ErrorResponseObject(string.Format(MorpheusMoviesConstants.ResponseConstants.PARAMETER_NOT_DEFINED, nameof(email))));

            var record = await _userRepository.GetByNameAsync(email);
            return new OkResponse<ApplicationUser>(record);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error accessing data in {nameof(GetApplicationUserByEmailAsync)}: {e.Message}");
            throw;
        }
    }

    public async Task<ResponseBase> SignIn(string email, string password)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return new KoResponse(new ErrorResponseObject(string.Format(MorpheusMoviesConstants.ResponseConstants.PARAMETER_NOT_DEFINED, $"{nameof(email)}, {nameof(password)}")));

            var user = await _userRepository.GetByNameAsync(email);
            if (user == null)
                return new KoResponse(new ErrorResponseObject(string.Format(MorpheusMoviesConstants.ResponseConstants.USER_NOT_FOUND, email)));

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                return new KoResponse(new ErrorResponseObject(MorpheusMoviesConstants.ResponseConstants.INVALID_CREDENTIALS));

            return new OkResponse<GeneralOkResponse>(new GeneralOkResponse(string.Format(MorpheusMoviesConstants.ResponseConstants.SIGNIN_SUCCESS, email)));
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error accessing data in {nameof(SignIn)}: {e.Message}");
            throw;
        }
    }

    public async Task<ResponseBase> SignUp(ApplicationUser newUser)
    {
        try
        {
            if (newUser == null || string.IsNullOrWhiteSpace(newUser.Email) || string.IsNullOrWhiteSpace(newUser.Password))
                return new KoResponse(new ErrorResponseObject(string.Format(MorpheusMoviesConstants.ResponseConstants.PARAMETER_NOT_DEFINED, $"{nameof(newUser.Email)}, {nameof(newUser.Password)}")));

            var existingUser = await _userRepository.GetByNameAsync(newUser.Email);
            if (existingUser != null)
                return new KoResponse(new ErrorResponseObject(string.Format(MorpheusMoviesConstants.ResponseConstants.USER_ALREADY_EXISTS, newUser.Email)));

            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            await _userRepository.CreateAsync(newUser);

            return new OkResponse<GeneralOkResponse>(new GeneralOkResponse(string.Format(MorpheusMoviesConstants.ResponseConstants.SIGNUP_SUCCESS, newUser.Email)));
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error accessing data in {nameof(SignUp)}: {e.Message}");
            throw;
        }
    }

    public async Task<ResponseBase> GetApplicationUsersByIdAsync(int id)
    {
        try
        {
            if (id <= 0)
                return new KoResponse(new ErrorResponseObject(string.Format(MorpheusMoviesConstants.ResponseConstants.PARAMETER_NOT_VALID, nameof(id))));

            var record = await _userRepository.GetByIdAsync(id);
            return new OkResponse<ApplicationUser>(record);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error accessing data in {nameof(GetApplicationUsersByIdAsync)}: {e.Message}");
            throw;
        }
    }
}
