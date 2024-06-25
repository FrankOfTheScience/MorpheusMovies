using MorpheusMovies.Server.EF.Model;

namespace MorpheusMovies.Server.Repository.Interfaces;

public interface IUserRepository : IRepository<ApplicationUser>
{
    Task<bool> CreateAsync();
    Task<ApplicationUser> UpdateAsync(ApplicationUser entity);
    Task<bool> DeleteAsync(int id);
}
