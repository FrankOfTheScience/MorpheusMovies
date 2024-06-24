using Microsoft.EntityFrameworkCore;
using MorpheusMovies.Server.EF;

namespace MorpheusMovies.Server.Utilities;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
        {
            if (context.Movies.Any() || context.Genres.Any() || context.Occupations.Any() || context.UserTrainingData.Any())
                return;

            var dataPath = Path.Combine(AppContext.BaseDirectory, "Data");

            var movies = DataSeeder.LoadMoviesFromCsv(Path.Combine(dataPath, "movie-data.csv"));
            var genres = DataSeeder.LoadGenresFromCsv(Path.Combine(dataPath, "genre-data.csv"));
            var occupations = DataSeeder.LoadOccupationsFromCsv(Path.Combine(dataPath, "occupation-data.csv"));
            var ratings = DataSeeder.LoadRatingsFromCsv(Path.Combine(dataPath, "rating-data.csv"));

            context.Movies.AddRange(movies);
            context.Genres.AddRange(genres);
            context.Occupations.AddRange(occupations);
            context.Ratings.AddRange(ratings);

            context.SaveChanges();
        }
    }
}
