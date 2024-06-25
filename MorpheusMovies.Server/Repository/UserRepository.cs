using MorpheusMovies.Server.EF;
using MorpheusMovies.Server.EF.Model;
using MorpheusMovies.Server.Repository.Interfaces;

namespace MorpheusMovies.Server.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context)
        => _context = context;

    public Task<bool> CreateAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ApplicationUser>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationUser> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationUser> GetByNameAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationUser> UpdateAsync(ApplicationUser entity)
    {
        throw new NotImplementedException();
    }
}
