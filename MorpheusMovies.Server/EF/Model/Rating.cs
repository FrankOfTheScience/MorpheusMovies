using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MorpheusMovies.Server.EF.Model;

[Table("Rating", Schema = "dbo")]
public class Rating
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RatingId { get; set; }
    [ForeignKey("User")]
    public int UserId { get; set; }
    [ForeignKey("Movie")]
    public int MovieId { get; set; }
    public float RatingValue { get; set; }
    public DateTime Timestamp { get; set; }
    public ApplicationUser User { get; set; }
    public Movie Movie { get; set; }
}