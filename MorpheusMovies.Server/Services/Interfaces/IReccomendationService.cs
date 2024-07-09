using MorpheusMovies.Server.ML.Model;

namespace MorpheusMovies.Server.Services.Interfaces;

public interface IReccomendationService
{
    Task<MovieRatingPrediction> GetReccomendationAsync(string email);
}
