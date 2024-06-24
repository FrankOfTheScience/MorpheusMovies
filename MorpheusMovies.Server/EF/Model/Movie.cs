using System.ComponentModel.DataAnnotations;

namespace MorpheusMovies.Server.EF.Model;

public class Movie
{
    [Key]
    public int MovieId { get; set; }
    public string Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string IMDbUrl { get; set; }
    public ICollection<Rating> Ratings { get; set; }
}