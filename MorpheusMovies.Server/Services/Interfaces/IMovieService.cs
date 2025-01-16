using MorpheusMovies.Server.EF.Model;

namespace MorpheusMovies.Server.Services.Interfaces;

public interface IMovieService
{
    Task<IEnumerable<Movie>> RetrieveAllMoviesAsync();
    Task<Movie> RetrieveMovieByIdAsync(int id);
    Task<Movie> RetrieveMovieByNameAsync(string name);
    Task<Movie> CreateMovieAsync(Movie movie);
    Task<Movie> UpdateMovieAsync(Movie movie);
    Task DeleteMovieAsync(int id);
}
