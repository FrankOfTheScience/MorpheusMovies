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

    public async Task<ResponseBase> RetrieveAllMoviesAsync()
    {
        try
        {
            var records = await _movieRepository.GetAllAsync();
            return new OkResponse<IEnumerable<Movie>>(records);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error accessing data in {nameof(RetrieveAllMoviesAsync)}: {e.Message}");
            throw;
        }
    }

    public async Task<ResponseBase> RetrieveMovieByIdAsync(int id)
    {
        try
        {
            if (id <= 0)
                return new KoResponse(new ErrorResponseObject(string.Format(MorpheusMoviesConstants.ResponseConstants.PARAMETER_NOT_VALID, nameof(id))));

            var record = await _movieRepository.GetByIdAsync(id);
            return new OkResponse<Movie>(record);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error accessing data in {nameof(RetrieveMovieByIdAsync)}: {e.Message}");
            throw;
        }
    }

    public async Task<ResponseBase> RetrieveMovieByNameAsync(string name)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                return new KoResponse(new ErrorResponseObject(string.Format(MorpheusMoviesConstants.ResponseConstants.PARAMETER_NOT_DEFINED, nameof(name))));

            var record = await _movieRepository.GetByNameAsync(name);
            return new OkResponse<Movie>(record);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error accessing data in {nameof(RetrieveMovieByNameAsync)}: {e.Message}");
            throw;
        }
    }
}
