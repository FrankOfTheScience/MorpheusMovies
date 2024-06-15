using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using MorpheusMovies.Server.MLModel;
using MorpheusMovies.Server.Models;

namespace MorpheusMovies.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class RecommendationsController : ControllerBase
{
    private readonly ITransformer _model;

    public RecommendationsController(ITransformer model)
        => _model = model;

    [HttpGet]
    public ActionResult<ModelOutput> GetRecommendations([FromQuery] float userId, [FromQuery] float movieId)
    {
        var prediction = ModelTraining.Predict(_model, userId, movieId);
        return Ok(new ModelOutput { MovieId = movieId, Prediction = prediction });
    }
}
