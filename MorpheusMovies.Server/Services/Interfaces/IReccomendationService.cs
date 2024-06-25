using MorpheusMovies.Server.DTOs;

namespace MorpheusMovies.Server.Services.Interfaces;

public interface IReccomendationService
{
    Task<ReccomendationResponse> GetReccomendationAsync(string email);
}
