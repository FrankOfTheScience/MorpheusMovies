using Microsoft.EntityFrameworkCore;
using MorpheusMovies.Server.EF;
using MorpheusMovies.Server.EF.Model;
using MorpheusMovies.Server.Repository.Interfaces;

namespace MorpheusMovies.Server.Repository;

public class MovieRepository : IMovieRepository
{
    private readonly ApplicationDbContext _context;
    public MovieRepository(ApplicationDbContext context)
        => _context = context;

    public async Task<IEnumerable<Movie>> GetAllAsync()
        => await _context.Movies.ToListAsync();

    public async Task<Movie> GetByIdAsync(int id)
        => await _context.Movies.FirstOrDefaultAsync(m => m.MovieId == id);

    public async Task<Movie> GetByNameAsync(string name)
        => await _context.Movies.FirstOrDefaultAsync(m => m.Title == name);
}
