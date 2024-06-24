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
                IMDbUrl = r.IMDbUrl
            }).ToList();
        }
    }

    public static List<Genre> LoadGenresFromCsv(string filePath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = "|",
            HasHeaderRecord = false
        };

        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, config))
        {
            var records = csv.GetRecords<GenreCsv>().ToList();
            return records.Select(r => new Genre
            {
                GenreId = r.GenreId,
                Name = r.Name
            }).ToList();
        }
    }

    public static List<Occupation> LoadOccupationsFromCsv(string filePath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = "|",
            HasHeaderRecord = false
        };

        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, config))
        {
            var records = csv.GetRecords<OccupationCsv>().ToList();
            return records.Select(r => new Occupation
            {
                OccupationId = r.OccupationId,
                Name = r.Name
            }).ToList();
        }
    }

    public static List<UserTrainingData> LoadUserTrainingDataFromCsv(string filePath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = "|",
            HasHeaderRecord = false
        };

        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, config))
        {
            var records = csv.GetRecords<UserTrainingDataCsv>().ToList();
            return records.Select(r => new UserTrainingData
            {
                UserId = r.UserId,
                Age = r.Age,
                Gender = r.Gender,
                Occupation = r.Occupation,
                ZipCode = r.ZipCode
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
}

public class GenreCsv
{
    public int GenreId { get; set; }
    public string Name { get; set; }
}

public class OccupationCsv
{
    public int OccupationId { get; set; }
    public string Name { get; set; }
}

public class UserTrainingDataCsv
{
    public int UserId { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public string Occupation { get; set; }
    public string ZipCode { get; set; }
}

public class RatingCsv
{
    public int UserId { get; set; }
    public int MovieId { get; set; }
    public float Rating { get; set; }
    public long Timestamp { get; set; }
}

