using CsvHelper;
using CsvHelper.Configuration;
using MorpheusMovies.Server.EF.Model;
using System.Globalization;

namespace MorpheusMovies.Server.Utilities;

public static class DataSeeder
{
    public static List<Movie> LoadMoviesFromCsv(string filePath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = "|",
            HasHeaderRecord = false
        };

        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, config))
        {
            var records = csv.GetRecords<MovieCsv>().ToList();
            return records.Select(r => new Movie
            {
                MovieId = r.MovieId,
                Title = r.Title,
                ReleaseDate = DateTime.Parse(r.ReleaseDate),
                IMDbUrl = r.IMDbUrl,
                Unknown = r.Unknown,
                Action = r.Action,
                Adventure = r.Adventure,
                Animation = r.Animation,
                Childrens = r.Childrens,
                Comedy = r.Comedy,
                Crime = r.Crime,
                Documentary = r.Documentary,
                Drama = r.Drama,
                Fantasy = r.Fantasy,
                FilmNoir = r.FilmNoir,
                Horror = r.Horror,
                Musical = r.Musical,
                Mystery = r.Mystery,
                Romance = r.Romance,
                SciFi = r.SciFi,
                Thriller = r.Thriller,
                War = r.War,
                Western = r.Western
            }).ToList();
        }
    }

    public static List<ApplicationUser> LoadUsersFromCsv(string filePath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = "|",
            HasHeaderRecord = false
        };

        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, config))
        {
            var records = csv.GetRecords<UserCsv>().ToList();
            return records.Select(r => new ApplicationUser
            {
                UserId = r.UserId,
                Age = r.Age,
                Gender = (Gender)Enum.Parse(typeof(Gender), r.Gender, true),
                Occupation = (Occupation)Enum.Parse(typeof(Occupation), r.Occupation, true),
            }).ToList();
        }
    }

    public static List<Rating> LoadRatingsFromCsv(string filePath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = "\t",
            HasHeaderRecord = false
        };

        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, config))
        {
            var records = csv.GetRecords<RatingCsv>().ToList();
            return records.Select(r => new Rating
            {
                UserId = r.UserId,
                MovieId = r.MovieId,
                RatingValue = r.Rating,
                Timestamp = DateTimeOffset.FromUnixTimeSeconds(r.Timestamp).DateTime
            }).ToList();
        }
    }
}

public class MovieCsv
{
    public int MovieId { get; set; }
    public string Title { get; set; }
    public string ReleaseDate { get; set; }
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
}

public class RatingCsv
{
    public int UserId { get; set; }
    public int MovieId { get; set; }
    public float Rating { get; set; }
    public long Timestamp { get; set; }
}

public class UserCsv
{
    public int UserId { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public string Occupation { get; set; }
    public string ZipCode { get; set; }
}

