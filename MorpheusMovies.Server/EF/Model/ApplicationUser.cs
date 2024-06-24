using System.ComponentModel.DataAnnotations;

namespace MorpheusMovies.Server.EF.Model;

public class ApplicationUser
{
    [Key]
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public string Occupation { get; set; }
    public string ZipCode { get; set; }
    public ICollection<UserMoviePreference> MoviePreferences { get; set; }
}
