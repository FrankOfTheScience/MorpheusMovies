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

    public async Task<Movie> CreateAsync(Movie entity)
    {
        await _context.Movies.AddAsync(entity);
        await _context.SaveChangesAsync();
        return await this.GetByIdAsync(entity.MovieId);
    }

    public async Task DeleteAsync(int id)
    {
        _context.Movies.Remove(await GetByIdAsync(id));
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Movie>> GetAllAsync()
        => await _context.Movies.ToListAsync();

    public async Task<Movie> GetByIdAsync(int id)
        => await _context.Movies.FirstOrDefaultAsync(m => m.MovieId == id);

    public async Task<Movie> GetByNameAsync(string name)
        => await _context.Movies.FirstOrDefaultAsync(m => m.Title == name);

    public async Task<Movie> UpdateAsync(Movie entity)
    {
        var entityToUpdate = await this.GetByIdAsync(entity.MovieId);

        var properties = entityToUpdate.GetType().GetProperties();
        foreach (var property in properties)
            property.SetValue(entityToUpdate, property.GetValue(entity));

        _context.Movies.Update(entityToUpdate);
        await _context.SaveChangesAsync();
        return entityToUpdate;
    }
}
