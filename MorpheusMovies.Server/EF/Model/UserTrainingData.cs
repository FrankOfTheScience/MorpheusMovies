using System.ComponentModel.DataAnnotations;

namespace MorpheusMovies.Server.EF.Model;

public class UserTrainingData
{
    [Key]
    public int UserId { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public string Occupation { get; set; }
    public string ZipCode { get; set; }
}
