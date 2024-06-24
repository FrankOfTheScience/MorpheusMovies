using System.ComponentModel.DataAnnotations.Schema;

namespace MorpheusMovies.Server.EF.Model;

public class Rating
{
    [ForeignKey("User")]
    public int UserId { get; set; }

    [ForeignKey("Movie")]
    public int MovieId { get; set; }

    public float RatingValue { get; set; }
    public DateTime Timestamp { get; set; }

    public ApplicationUser User { get; set; }
    public Movie Movie { get; set; }
}