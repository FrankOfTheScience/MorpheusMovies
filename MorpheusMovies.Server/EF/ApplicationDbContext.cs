using Microsoft.EntityFrameworkCore;
using MorpheusMovies.Server.EF.Model;

namespace MorpheusMovies.Server.EF;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Occupation> Occupations { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<UserTrainingData> UserTrainingData { get; set; }
    public DbSet<UserMoviePreference> UserMoviePreferences { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>()
            .HasKey(u => u.UserId);

        modelBuilder.Entity<Movie>()
            .HasKey(m => m.MovieId);

        modelBuilder.Entity<Genre>()
            .HasKey(g => g.GenreId);

        modelBuilder.Entity<Occupation>()
            .HasKey(o => o.OccupationId);

        modelBuilder.Entity<Rating>()
            .HasKey(r => new { r.UserId, r.MovieId });

        modelBuilder.Entity<Rating>()
            .HasOne(r => r.Movie)
            .WithMany()
            .HasForeignKey(r => r.MovieId);

        modelBuilder.Entity<UserTrainingData>()
            .HasKey(u => u.UserId);

        modelBuilder.Entity<UserMoviePreference>()
            .HasKey(ump => ump.UserMoviePreferenceId);

        modelBuilder.Entity<UserMoviePreference>()
            .HasOne(ump => ump.User)
            .WithMany(u => u.MoviePreferences)
            .HasForeignKey(ump => ump.UserId);

        modelBuilder.Entity<UserMoviePreference>()
            .HasOne(ump => ump.Movie)
            .WithMany()
            .HasForeignKey(ump => ump.MovieId);
    }
}

