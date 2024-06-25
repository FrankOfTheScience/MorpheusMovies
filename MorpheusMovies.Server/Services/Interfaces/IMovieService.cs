using MorpheusMovies.Server.EF.Model;

namespace MorpheusMovies.Server.Services.Interfaces;

public interface IMovieService
{
    Task<IEnumerable<Movie>> RetrieveAllMoviesAsync();
    Task<Movie> RetrieveMovieByIdAsync(string id);
    Task<Movie> RetrieveMovieByNameAsync(string name);
}
