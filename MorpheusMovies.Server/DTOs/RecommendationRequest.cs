namespace MorpheusMovies.Server.DTOs;

public class RecommendationRequest
{
    public int UserId { get; set; }
    public List<int> MovieIds { get; set; }
}
