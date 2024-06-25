using MorpheusMovies.Server.EF.Model;
using MorpheusMovies.Server.Repository.Interfaces;
using MorpheusMovies.Server.Services.Interfaces;

namespace MorpheusMovies.Server.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    public MovieService(IMovieRepository movieRepository)
        => _movieRepository = movieRepository;

    public Task<IEnumerable<Movie>> RetrieveAllMoviesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Movie> RetrieveMovieByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<Movie> RetrieveMovieByNameAsync(string name)
    {
        throw new NotImplementedException();
    }
}
