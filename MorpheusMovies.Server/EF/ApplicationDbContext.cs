using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using MorpheusMovies.Server.EF.Model;
using MorpheusMovies.Server.Utilities;

namespace MorpheusMovies.Server.EF;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    { }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        var dbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
        if (dbCreator != null)
        {
            if (!dbCreator.CanConnect()) dbCreator.Create();
            if (!dbCreator.HasTables()) dbCreator.CreateTables();
        }
    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<UserMoviePreference> UserMoviePreferences { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var dataPath = Path.Combine(AppContext.BaseDirectory, "Data");

        var movies = DataSeeder.LoadMoviesFromCsv(Path.Combine(dataPath, "movie-data.csv"));
        var ratings = DataSeeder.LoadRatingsFromCsv(Path.Combine(dataPath, "rating-data.csv"));
        var users = DataSeeder.LoadUsersFromCsv(Path.Combine(dataPath, "user-data.csv"));

        modelBuilder.Entity<Movie>().HasData(movies);
        modelBuilder.Entity<Rating>().HasData(ratings);
        modelBuilder.Entity<ApplicationUser>().HasData(users);
    }
}

