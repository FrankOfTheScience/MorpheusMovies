using BCrypt.Net;
using MorpheusMovies.Server.CustomException;
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

    public async Task ChangePasswordAsync(string email, string newPassword)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(newPassword))
                throw new ErrorInfoException(new ErrorResponseObject(string.Format(MorpheusMoviesConstants.ResponseConstants.PARAMETER_NOT_DEFINED, $"{nameof(email)}, {nameof(newPassword)}")), MorpheusMoviesConstants.ResponseConstants.CLIENT_ERROR_CODE);

            var user = await _userRepository.GetByNameAsync(email);
            if (user == null)
                throw new ErrorInfoException(new ErrorResponseObject(string.Format(MorpheusMoviesConstants.ResponseConstants.PARAMETER_NOT_DEFINED, $"{nameof(email)}, {nameof(newPassword)}")), MorpheusMoviesConstants.ResponseConstants.CLIENT_ERROR_CODE);

            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _userRepository.UpdateAsync(user);
        }
        catch (KeyNotFoundException e)
        {
            Console.WriteLine($"Entity not found in {nameof(ChangePasswordAsync)}: {e.Message}");
            throw new ErrorInfoException(new ErrorResponseObject(e.Message));
        }
        catch (SaltParseException e)
        {
            Console.WriteLine($"Error hashing password {nameof(ChangePasswordAsync)}: {e.Message}");
            throw new ErrorInfoException(new ErrorResponseObject(MorpheusMoviesConstants.ResponseConstants.GENERAL_ERROR, MorpheusMoviesConstants.ResponseConstants.SERVER_ERROR_CODE), e.Message, e);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error accessing data in {nameof(ChangePasswordAsync)}: {e.Message}");
            throw new ErrorInfoException(new ErrorResponseObject(MorpheusMoviesConstants.ResponseConstants.GENERAL_ERROR, MorpheusMoviesConstants.ResponseConstants.SERVER_ERROR_CODE), e.Message, e);
        }
    }

    public async Task DeleteUserProfileAsync(string email)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ErrorInfoException(new ErrorResponseObject(string.Format(MorpheusMoviesConstants.ResponseConstants.PARAMETER_NOT_DEFINED, nameof(email)), MorpheusMoviesConstants.ResponseConstants.CLIENT_ERROR_CODE));

            var user = await _userRepository.GetByNameAsync(email);
            if (user is null)
                throw new ErrorInfoException(new ErrorResponseObject(string.Format(MorpheusMoviesConstants.ResponseConstants.ENTITY_NOT_FOUND_FOR_THE_OPERATION, nameof(ApplicationUser), MorpheusMoviesConstants.CRUD_DELETE)), MorpheusMoviesConstants.ResponseConstants.CLIENT_ERROR_CODE);

            await _userRepository.DeleteAsync(user.UserId);
        }
        catch (KeyNotFoundException e)
        {
            Console.WriteLine($"Entity not found in {nameof(DeleteUserProfileAsync)}: {e.Message}");
            throw new ErrorInfoException(new ErrorResponseObject(e.Message));
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error accessing data in {nameof(DeleteUserProfileAsync)}: {e.Message}");
            throw new ErrorInfoException(new ErrorResponseObject(MorpheusMoviesConstants.ResponseConstants.GENERAL_ERROR, MorpheusMoviesConstants.ResponseConstants.SERVER_ERROR_CODE), e.Message, e);
        }
    }

    public async Task EditUserProfileAsync(ApplicationUser editedUser)
    {
        try
        {
            if (editedUser is null)
                throw new ErrorInfoException(new ErrorResponseObject(string.Format(MorpheusMoviesConstants.ResponseConstants.PARAMETER_NOT_DEFINED, nameof(ApplicationUser)), MorpheusMoviesConstants.ResponseConstants.CLIENT_ERROR_CODE));

            await _userRepository.UpdateAsync(editedUser);
        }
        catch (KeyNotFoundException e)
        {
            Console.WriteLine($"Entity not found in {nameof(EditUserProfileAsync)}: {e.Message}");
            throw new ErrorInfoException(new ErrorResponseObject(e.Message));
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error accessing data in {nameof(EditUserProfileAsync)}: {e.Message}");
            throw new ErrorInfoException(new ErrorResponseObject(MorpheusMoviesConstants.ResponseConstants.GENERAL_ERROR, MorpheusMoviesConstants.ResponseConstants.SERVER_ERROR_CODE), e.Message, e);
        }
    }

    public async Task<IEnumerable<ApplicationUser>> GetApplicationUsersAsync()
    {
        try
        {
            return await _userRepository.GetAllAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error accessing data in {nameof(GetApplicationUsersAsync)}: {e.Message}");
            throw new ErrorInfoException(new ErrorResponseObject(MorpheusMoviesConstants.ResponseConstants.GENERAL_ERROR, MorpheusMoviesConstants.ResponseConstants.SERVER_ERROR_CODE), e.Message, e);
        }
    }

    public async Task<ApplicationUser> GetApplicationUserByEmailAsync(string email)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ErrorInfoException(new ErrorResponseObject(string.Format(MorpheusMoviesConstants.ResponseConstants.PARAMETER_NOT_DEFINED, nameof(email)), MorpheusMoviesConstants.ResponseConstants.CLIENT_ERROR_CODE));

            return await _userRepository.GetByNameAsync(email);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error accessing data in {nameof(GetApplicationUserByEmailAsync)}: {e.Message}");
            throw new ErrorInfoException(new ErrorResponseObject(MorpheusMoviesConstants.ResponseConstants.GENERAL_ERROR, MorpheusMoviesConstants.ResponseConstants.SERVER_ERROR_CODE), e.Message, e);
        }
    }

    public async Task SignIn(string email, string password)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                throw new ErrorInfoException(new ErrorResponseObject(string.Format(MorpheusMoviesConstants.ResponseConstants.PARAMETER_NOT_DEFINED, $"{nameof(email)}, {nameof(password)}"), MorpheusMoviesConstants.ResponseConstants.CLIENT_ERROR_CODE));

            var user = await _userRepository.GetByNameAsync(email);
            if (user == null)
                throw new ErrorInfoException(new ErrorResponseObject(string.Format(MorpheusMoviesConstants.ResponseConstants.USER_NOT_FOUND_BY_EMAIL, email)), MorpheusMoviesConstants.ResponseConstants.CLIENT_ERROR_CODE);

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                throw new ErrorInfoException(new ErrorResponseObject(MorpheusMoviesConstants.ResponseConstants.INVALID_CREDENTIALS), MorpheusMoviesConstants.ResponseConstants.CLIENT_ERROR_CODE);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error accessing data in {nameof(SignIn)}: {e.Message}");
            throw new ErrorInfoException(new ErrorResponseObject(MorpheusMoviesConstants.ResponseConstants.GENERAL_ERROR, MorpheusMoviesConstants.ResponseConstants.SERVER_ERROR_CODE), e.Message, e);
        }
    }

    public async Task SignUp(ApplicationUser newUser)
    {
        try
        {
            if (newUser == null || string.IsNullOrWhiteSpace(newUser.Email) || string.IsNullOrWhiteSpace(newUser.Password))
                throw new ErrorInfoException(new ErrorResponseObject(string.Format(MorpheusMoviesConstants.ResponseConstants.PARAMETER_NOT_DEFINED, $"{nameof(newUser.Email)}, {nameof(newUser.Password)}")), MorpheusMoviesConstants.ResponseConstants.CLIENT_ERROR_CODE);

            var existingUser = await _userRepository.GetByNameAsync(newUser.Email);
            if (existingUser != null)
                throw new ErrorInfoException(new ErrorResponseObject(string.Format(MorpheusMoviesConstants.ResponseConstants.USER_ALREADY_EXISTS, newUser.Email), MorpheusMoviesConstants.ResponseConstants.CLIENT_ERROR_CODE));

            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            await _userRepository.CreateAsync(newUser);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error accessing data in {nameof(SignUp)}: {e.Message}");
            throw new ErrorInfoException(new ErrorResponseObject(MorpheusMoviesConstants.ResponseConstants.GENERAL_ERROR, MorpheusMoviesConstants.ResponseConstants.SERVER_ERROR_CODE), e.Message, e);
        }
    }

    public async Task<ApplicationUser> GetApplicationUsersByIdAsync(int id)
    {
        try
        {
            if (id <= 0)
                throw new ErrorInfoException(new ErrorResponseObject(string.Format(MorpheusMoviesConstants.ResponseConstants.PARAMETER_NOT_VALID, nameof(id)), MorpheusMoviesConstants.ResponseConstants.CLIENT_ERROR_CODE));

            return await _userRepository.GetByIdAsync(id);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error accessing data in {nameof(GetApplicationUsersByIdAsync)}: {e.Message}");
            throw new ErrorInfoException(new ErrorResponseObject(MorpheusMoviesConstants.ResponseConstants.GENERAL_ERROR, MorpheusMoviesConstants.ResponseConstants.SERVER_ERROR_CODE), e.Message, e);
        }
    }
}
