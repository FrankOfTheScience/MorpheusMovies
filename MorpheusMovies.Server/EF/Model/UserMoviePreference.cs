using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MorpheusMovies.Server.EF.Model;

[Table("UserMoviePreference", Schema = "dbo")]
public class UserMoviePreference
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserMoviePreferenceId { get; set; }
    [ForeignKey("UserId")]
    public int UserId { get; set; }
    [ForeignKey("MovieId")]
    public int MovieId { get; set; }
    public ApplicationUser User { get; set; }
    public Movie Movie { get; set; }
}
