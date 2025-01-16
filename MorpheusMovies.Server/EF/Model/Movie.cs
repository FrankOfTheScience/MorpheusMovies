using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MorpheusMovies.Server.EF.Model;

[Table("Movie", Schema = "dbo")]
public class Movie
{
    [Key]
    public int MovieId { get; set; }
    public string Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string IMDbUrl { get; set; }
    public bool Unknown { get; set; }
    public bool Action { get; set; }
    public bool Adventure { get; set; }
    public bool Animation { get; set; }
    public bool Childrens { get; set; }
    public bool Comedy { get; set; }
    public bool Crime { get; set; }
    public bool Documentary { get; set; }
    public bool Drama { get; set; }
    public bool Fantasy { get; set; }
    public bool FilmNoir { get; set; }
    public bool Horror { get; set; }
    public bool Musical { get; set; }
    public bool Mystery { get; set; }
    public bool Romance { get; set; }
    public bool SciFi { get; set; }
    public bool Thriller { get; set; }
    public bool War { get; set; }
    public bool Western { get; set; }
    public virtual IEnumerable<Rating> Ratings { get; set; } = new List<Rating>();
    public virtual IEnumerable<UserMoviePreference> UserPreferences { get; set; } = new List<UserMoviePreference>();
}

public enum Genre
{
    unknown = 0,
    Action = 1,
    Adventure = 2,
    Animation = 3,
    Childrens = 4,  //Children's
    Comedy = 5,
    Crime = 6,
    Documentary = 7,
    Drama = 8,
    Fantasy = 9,
    FilmNoir = 10,  //Film-Noir
    Horror = 11,
    Musical = 12,
    Mystery = 13,
    Romance = 14,
    SciFi = 15,     //Sci-Fi
    Thriller = 16,
    War = 17,
    Western = 18
}