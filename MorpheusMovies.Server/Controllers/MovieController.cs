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
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;
    public MovieController(IMovieService movieService)
        => _movieService = movieService;

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetMovies()
    {
        try
        {
            var movies = await _movieService.RetrieveAllMoviesAsync();
            return Ok(new OkResponse<IEnumerable<Movie>>(movies));
        }
        catch (ErrorInfoException e)
        {
            return ApiUtilities.GenerateKoResponse(e.ErrorResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unexpected error in {nameof(MovieController)}: {e.Message}");
            return new InternalServerErrorObjectResult(new ErrorResponseObject(MorpheusMoviesConstants.ResponseConstants.GENERAL_ERROR, MorpheusMoviesConstants.ResponseConstants.SERVER_ERROR_CODE));
        }
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetMovieById(int id)
    {
        try
        {
            var movie = await _movieService.RetrieveMovieByIdAsync(id);
            if (movie is null)
                return NotFound(new KoResponse(new ErrorResponseObject(string.Format(MorpheusMoviesConstants.ResponseConstants.MOVIE_NOT_FOUND_BY_ID), id.ToString())));
            return Ok(new OkResponse<Movie>(movie));
        }
        catch (ErrorInfoException e)
        {
            return ApiUtilities.GenerateKoResponse(e.ErrorResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unexpected error in {nameof(MovieController)}: {e.Message}");
            return new InternalServerErrorObjectResult(new ErrorResponseObject(MorpheusMoviesConstants.ResponseConstants.GENERAL_ERROR, MorpheusMoviesConstants.ResponseConstants.SERVER_ERROR_CODE));
        }
    }

    [Authorize]
    [HttpGet("name")]
    public async Task<IActionResult> GetMovieByEmail([FromQuery] string name)
    {
        try
        {
            var movie = await _movieService.RetrieveMovieByNameAsync(name);
            if (movie is null)
                return NotFound(new KoResponse(new ErrorResponseObject(string.Format(MorpheusMoviesConstants.ResponseConstants.MOVIE_NOT_FOUND_BY_NAME), name)));
            return Ok(new OkResponse<Movie>(movie));
        }
        catch (ErrorInfoException e)
        {
            return ApiUtilities.GenerateKoResponse(e.ErrorResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unexpected error in {nameof(MovieController)}: {e.Message}");
            return new InternalServerErrorObjectResult(new ErrorResponseObject(MorpheusMoviesConstants.ResponseConstants.GENERAL_ERROR, MorpheusMoviesConstants.ResponseConstants.SERVER_ERROR_CODE));
        }
    }
}
