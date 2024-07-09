using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MorpheusMovies.Server.CustomException;
using MorpheusMovies.Server.DTOs;
using MorpheusMovies.Server.EF.Model;
using MorpheusMovies.Server.Services.Interfaces;
using MorpheusMovies.Server.Utilities;

namespace MorpheusMovies.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ApplicationUserController : ControllerBase
{
    private readonly IUserService _userService;
    public ApplicationUserController(IUserService userService)
        => _userService = userService;

    [HttpGet]
    public async Task<IActionResult> GetApplicationUsers()
    {
        try
        {
            var users = await _userService.GetApplicationUsersAsync();
            return Ok(new OkResponse<IEnumerable<ApplicationUser>>(users));
        }
        catch (ErrorInfoException e)
        {
            return ApiUtilities.GenerateKoResponse(e.ErrorResponse);
        }
        catch (Exception ex)
        {
            return new InternalServerErrorObjectResult(new ErrorResponseObject(MorpheusMoviesConstants.ResponseConstants.GENERAL_ERROR, MorpheusMoviesConstants.ResponseConstants.SERVER_ERROR_CODE));
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetApplicationUserById(int id)
    {
        try
        {
            var user = await _userService.GetApplicationUsersByIdAsync(id);
            if (user is null)
                return NotFound(new KoResponse(new ErrorResponseObject(string.Format(MorpheusMoviesConstants.ResponseConstants.USER_NOT_FOUND_BY_ID), id.ToString())));
            return Ok(new OkResponse<ApplicationUser>(user));
        }
        catch (ErrorInfoException e)
        {
            return ApiUtilities.GenerateKoResponse(e.ErrorResponse);
        }
        catch (Exception e)
        {
            return new InternalServerErrorObjectResult(new ErrorResponseObject(MorpheusMoviesConstants.ResponseConstants.GENERAL_ERROR, MorpheusMoviesConstants.ResponseConstants.SERVER_ERROR_CODE));
        }
    }

    [HttpGet("email")]
    public async Task<IActionResult> GetApplicationUserByEmail([FromQuery] string email)
    {
        try
        {
            var user = await _userService.GetApplicationUserByEmailAsync(email);
            if (user is null)
                return NotFound(new KoResponse(new ErrorResponseObject(string.Format(MorpheusMoviesConstants.ResponseConstants.USER_NOT_FOUND_BY_EMAIL), email)));
            return Ok(new OkResponse<ApplicationUser>(user));
        }
        catch (ErrorInfoException e)
        {
            return ApiUtilities.GenerateKoResponse(e.ErrorResponse);
        }
        catch (Exception ex)
        {
            return new InternalServerErrorObjectResult(new ErrorResponseObject(MorpheusMoviesConstants.ResponseConstants.GENERAL_ERROR, MorpheusMoviesConstants.ResponseConstants.SERVER_ERROR_CODE));
        }
    }

    [HttpPut]
    public async Task<IActionResult> EditUserProfile(ApplicationUser editedUser)
    {
        try
        {
            await _userService.EditUserProfileAsync(editedUser);
            return Ok(new GeneralOkResponse("TODO CONSTANT"));
        }
        catch (ErrorInfoException e)
        {
            return ApiUtilities.GenerateKoResponse(e.ErrorResponse);
        }
        catch (Exception ex)
        {
            return new InternalServerErrorObjectResult(new ErrorResponseObject(MorpheusMoviesConstants.ResponseConstants.GENERAL_ERROR, MorpheusMoviesConstants.ResponseConstants.SERVER_ERROR_CODE));
        }
    }
    [HttpDelete("{email}")]
    public async Task<IActionResult> DeleteUserProfile(string email)
    {
        try
        {
            await _userService.DeleteUserProfileAsync(email);
            return Ok(new GeneralOkResponse("TODO CONSTANT"));
        }
        catch (ErrorInfoException e)
        {
            return ApiUtilities.GenerateKoResponse(e.ErrorResponse);
        }
        catch (Exception ex)
        {
            return new InternalServerErrorObjectResult(new ErrorResponseObject(MorpheusMoviesConstants.ResponseConstants.GENERAL_ERROR, MorpheusMoviesConstants.ResponseConstants.SERVER_ERROR_CODE));
        }
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp(ApplicationUser newUser)
    {
        try
        {
            await _userService.SignUp(newUser);
            return Ok(new GeneralOkResponse("TODO CONSTANT"));
        }
        catch (ErrorInfoException e)
        {
            return ApiUtilities.GenerateKoResponse(e.ErrorResponse);
        }
        catch (Exception ex)
        {
            return new InternalServerErrorObjectResult(new ErrorResponseObject(MorpheusMoviesConstants.ResponseConstants.GENERAL_ERROR, MorpheusMoviesConstants.ResponseConstants.SERVER_ERROR_CODE));
        }
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] SignInRequest signInRequest)
    {
        try
        {
            await _userService.SignIn(signInRequest.Email, signInRequest.Password);
            return Ok(new GeneralOkResponse("TODO CONSTANT"));
        }
        catch (ErrorInfoException e)
        {
            return ApiUtilities.GenerateKoResponse(e.ErrorResponse);
        }
        catch (Exception ex)
        {
            return new InternalServerErrorObjectResult(new ErrorResponseObject(MorpheusMoviesConstants.ResponseConstants.GENERAL_ERROR, MorpheusMoviesConstants.ResponseConstants.SERVER_ERROR_CODE));
        }
    }

    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest)
    {
        try
        {
            await _userService.ChangePasswordAsync(changePasswordRequest.Email, changePasswordRequest.NewPassword);
            var updatedUser = await _userService.GetApplicationUserByEmailAsync(changePasswordRequest.Email);
            return Ok(new OkResponse<ApplicationUser>(updatedUser));
        }
        catch (ErrorInfoException e)
        {
            return ApiUtilities.GenerateKoResponse(e.ErrorResponse);
        }
        catch (Exception ex)
        {
            return new InternalServerErrorObjectResult(new ErrorResponseObject(MorpheusMoviesConstants.ResponseConstants.GENERAL_ERROR, MorpheusMoviesConstants.ResponseConstants.SERVER_ERROR_CODE));
        }
    }
}
