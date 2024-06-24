using System.ComponentModel.DataAnnotations;

namespace MorpheusMovies.Server.EF.Model;

public class Genre
{
    [Key]
    public int GenreId { get; set; }
    public string Name { get; set; }
}