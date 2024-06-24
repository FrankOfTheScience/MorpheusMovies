using System.ComponentModel.DataAnnotations;

namespace MorpheusMovies.Server.EF.Model;

public class Occupation
{
    [Key]
    public int OccupationId { get; set; }
    public string Name { get; set; }
}