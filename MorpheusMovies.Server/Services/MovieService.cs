using MorpheusMovies.Server.CustomException;
using MorpheusMovies.Server.DTOs;
using MorpheusMovies.Server.EF.Model;
using MorpheusMovies.Server.Repository.Interfaces;
using MorpheusMovies.Server.Services.Interfaces;
using MorpheusMovies.Server.Utilities;

namespace MorpheusMovies.Server.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    public MovieService(IMovieRepository movieRepository)
        => _movieRepository = movieRepository;

    public async Task<Movie> CreateMovieAsync(Movie movie)
    {
        try
        {
            if (movie is null)
                throw new ErrorInfoException(new ErrorResponseObject(MorpheusMoviesConstants.ResponseConstants.BODY_IS_NULL), MorpheusMoviesConstants.ResponseConstants.CLIENT_ERROR_CODE);

            return await _movieRepository.CreateAsync(movie);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error accessing data in {nameof(RetrieveMovieByNameAsync)}: {e.Message}");
            throw new ErrorInfoException(new ErrorResponseObject(MorpheusMoviesConstants.ResponseConstants.GENERAL_ERROR, MorpheusMoviesConstants.ResponseConstants.SERVER_ERROR_CODE), e.Message, e);
        }
    }

    public async Task DeleteMovieAsync(int id)
    {
        try
        {
            if (id <= 0)
                throw new ErrorInfoException(new ErrorResponseObject(string.Format(MorpheusMoviesConstants.ResponseConstants.PARAMETER_NOT_VALID, nameof(id))), MorpheusMoviesConstants.ResponseConstants.CLIENT_ERROR_CODE);

            await _movieRepository.DeleteAsync(id);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error accessing data in {nameof(RetrieveMovieByNameAsync)}: {e.Message}");
            throw new ErrorInfoException(new ErrorResponseObject(MorpheusMoviesConstants.ResponseConstants.GENERAL_ERROR, MorpheusMoviesConstants.ResponseConstants.SERVER_ERROR_CODE), e.Message, e);
        }
    }

    public async Task<IEnumerable<Movie>> RetrieveAllMoviesAsync()
    {
        try
        {
            return await _movieRepository.GetAllAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error accessing data in {nameof(RetrieveAllMoviesAsync)}: {e.Message}");
            throw new ErrorInfoException(new ErrorResponseObject(MorpheusMoviesConstants.ResponseConstants.GENERAL_ERROR, MorpheusMoviesConstants.ResponseConstants.SERVER_ERROR_CODE), e.Message, e);
        }
    }

    public async Task<Movie> RetrieveMovieByIdAsync(int id)
    {
        try
        {
            if (id <= 0)
                throw new ErrorInfoException(new ErrorResponseObject(string.Format(MorpheusMoviesConstants.ResponseConstants.PARAMETER_NOT_VALID, nameof(id)), MorpheusMoviesConstants.ResponseConstants.CLIENT_ERROR_CODE));

            return await _movieRepository.GetByIdAsync(id);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error accessing data in {nameof(RetrieveMovieByIdAsync)}: {e.Message}");
            throw new ErrorInfoException(new ErrorResponseObject(MorpheusMoviesConstants.ResponseConstants.GENERAL_ERROR, MorpheusMoviesConstants.ResponseConstants.SERVER_ERROR_CODE)), e.Message, e);
        }
    }

    public async Task<Movie> RetrieveMovieByNameAsync(string name)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ErrorInfoException(new ErrorResponseObject(string.Format(MorpheusMoviesConstants.ResponseConstants.PARAMETER_NOT_DEFINED, nameof(name)), MorpheusMoviesConstants.ResponseConstants.CLIENT_ERROR_CODE));

            return await _movieRepository.GetByNameAsync(name);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error accessing data in {nameof(RetrieveMovieByNameAsync)}: {e.Message}");
            throw new ErrorInfoException(new ErrorResponseObject(MorpheusMoviesConstants.ResponseConstants.GENERAL_ERROR, MorpheusMoviesConstants.ResponseConstants.SERVER_ERROR_CODE), e.Message, e);
        }
    }

    public async Task<Movie> UpdateMovieAsync(Movie movie)
    {
        try
        {
            if (movie is null)
                throw new ErrorInfoException(new ErrorResponseObject(MorpheusMoviesConstants.ResponseConstants.BODY_IS_NULL), MorpheusMoviesConstants.ResponseConstants.CLIENT_ERROR_CODE);

            return await _movieRepository.UpdateAsync(movie);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error accessing data in {nameof(RetrieveMovieByNameAsync)}: {e.Message}");
            throw new ErrorInfoException(new ErrorResponseObject(MorpheusMoviesConstants.ResponseConstants.GENERAL_ERROR, MorpheusMoviesConstants.ResponseConstants.SERVER_ERROR_CODE), e.Message, e);
        }
    }
}
