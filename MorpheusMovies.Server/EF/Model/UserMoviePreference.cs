using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MorpheusMovies.Server.EF.Model;

public class UserMoviePreference
{
    [Key]
    public int UserMoviePreferenceId { get; set; }
    [ForeignKey("UserId")]
    public int UserId { get; set; }
    [ForeignKey("MovieId")]
    public int MovieId { get; set; }
    public ApplicationUser User { get; set; }
    public Movie Movie { get; set; }
}
