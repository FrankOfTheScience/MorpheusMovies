using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
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
        // 1. Recupera l'utente dal contesto
        var user = _context.ApplicationUsers
            .Include(u => u.MoviePreferences) // Assumendo che ci sia una relazione con le preferenze
            .FirstOrDefault(u => u.UserId == userId);

        if (user == null)
            return NotFound($"User with ID {userId} not found");

        // 2. Verifica se ci sono preferenze
        var preferences = user.MoviePreferences.Select(p => p.MovieId).ToList();
        if (!preferences.Any())
            return BadRequest("User has no preferences to base recommendations on");

        var recomendation = MLModel.ApplicationMLModel.Predict(_mLContext, _model, user);

        // 3. Passa i dati al modello di ML
        var inputData = new MLModel.ModelInput
        {
            MovieIds = preferences // Supponendo che il modello supporti un array di ID film
        };

        var recommendations = MLModel.ApplicationMLModel.Predict(_mLContext, _model, inputData);

        // 4. Restituisci le raccomandazioni
        return Ok(recommendations);
    }
}
