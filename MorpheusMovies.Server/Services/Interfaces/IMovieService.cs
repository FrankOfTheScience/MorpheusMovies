using MorpheusMovies.Server.DTOs;

namespace MorpheusMovies.Server.Services.Interfaces;

public interface IMovieService
{
    Task<ResponseBase> RetrieveAllMoviesAsync();
    Task<ResponseBase> RetrieveMovieByIdAsync(int id);
    Task<ResponseBase> RetrieveMovieByNameAsync(string name);
}
