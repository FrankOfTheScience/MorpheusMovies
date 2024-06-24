namespace MorpheusMovies.Server.ML.Model;

public class MovieRating
{
    public float UserId { get; set; }
    public float MovieId { get; set; }
    public float Rating { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public string Occupation { get; set; }
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
}
