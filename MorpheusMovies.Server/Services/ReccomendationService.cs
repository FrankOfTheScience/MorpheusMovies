using Microsoft.ML;
using MorpheusMovies.Server.ML.Model;
using MorpheusMovies.Server.Repository.Interfaces;
using MorpheusMovies.Server.Services.Interfaces;

namespace MorpheusMovies.Server.Services;

public class ReccomendationService : IReccomendationService
{
    private readonly IUserRepository _userRepository;
    private readonly ITransformer _model;
    private readonly MLContext _mLContext;
    public ReccomendationService(IUserRepository userRepository, ITransformer model, MLContext mlContext)
    {
        _userRepository = userRepository;
        _model = model;
        _mLContext = mlContext;
    }

    public async Task<MovieRatingPrediction> GetReccomendationAsync(string email)
    {
        var user = await _userRepository.GetByNameAsync(email);
        if (user == null)
            throw new Exception();

        var reccomendation = MLModel.ApplicationMLModel.Predict(_mLContext, _model, user);

        //TODO: Finish to implement
        throw new NotImplementedException();
    }
}
