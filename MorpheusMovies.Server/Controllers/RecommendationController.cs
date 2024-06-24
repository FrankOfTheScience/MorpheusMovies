using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using MorpheusMovies.Server.DTOs;
using MorpheusMovies.Server.EF;
using MorpheusMovies.Server.MLModel;

namespace MorpheusMovies.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RecommendationController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ITransformer _model;
    private readonly MLContext _mLContext;

    public RecommendationController(ApplicationDbContext context, MLContext mlContext, ITransformer model)
    {
        _context = context;
        _mLContext = mlContext;
        _model = model;
    }

    [HttpPost]
    public IActionResult GetRecommendations([FromBody] RecommendationRequest request)
    {
        var recomendation = ModelTraining.Predict(_mLContext, _model, request.UserId, request.MovieIds);

        return Ok(new ReccomendationResponse { SuggestedMovies = recomendation });
    }
}
