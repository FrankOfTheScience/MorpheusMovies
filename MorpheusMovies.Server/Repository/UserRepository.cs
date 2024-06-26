using Microsoft.EntityFrameworkCore;
using MorpheusMovies.Server.EF;
using MorpheusMovies.Server.EF.Model;
using MorpheusMovies.Server.Repository.Interfaces;
using MorpheusMovies.Server.Utilities;

namespace MorpheusMovies.Server.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context)
        => _context = context;

    public async Task CreateAsync(ApplicationUser user)
    {
        _context.ApplicationUsers.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _context.ApplicationUsers.FindAsync(id);
        if (user is null)
            throw new KeyNotFoundException(string.Format(MorpheusMoviesConstants.ResponseConstants.ENTITY_NOT_FOUND_FOR_THE_OPERATION, nameof(ApplicationUser), MorpheusMoviesConstants.CRUD_DELETE));

        _context.ApplicationUsers.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        => await _context.ApplicationUsers.ToListAsync();

    public async Task<ApplicationUser> GetByIdAsync(int id)
        => await _context.ApplicationUsers.FindAsync(id);

    public async Task<ApplicationUser> GetByNameAsync(string email)
        => await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<ApplicationUser> UpdateAsync(ApplicationUser entity)
    {
        var user = await _context.ApplicationUsers.FindAsync(entity.UserId);
        if (user is null)
            throw new KeyNotFoundException(string.Format(MorpheusMoviesConstants.ResponseConstants.ENTITY_NOT_FOUND_FOR_THE_OPERATION, nameof(ApplicationUser), MorpheusMoviesConstants.CRUD_PUT));

        foreach (var property in entity.GetType().GetProperties())
        {
            var value = property.GetValue(entity);
            if (value is not null)
                property.SetValue(user, value);
        }

        await _context.SaveChangesAsync();
        return user;
    }
}
