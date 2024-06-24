using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using MorpheusMovies.Server.DTOs;
using MorpheusMovies.Server.EF;

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

    [HttpGet("userId:int")]
    public IActionResult GetRecommendations(int userId)
    {
        //TODO: Implementare business layer, repository layer, interfaces
        var user = _context.ApplicationUsers.Find(userId);
        var recomendation = MLModel.ApplicationMLModel.Predict(_mLContext, _model, user);

        return Ok(new ReccomendationResponse { SuggestedMovies = recomendation });
    }
}
