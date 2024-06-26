using MorpheusMovies.Server.EF.Model;

namespace MorpheusMovies.Server.Repository.Interfaces;

public interface IUserRepository : IRepository<ApplicationUser>
{
    Task CreateAsync(ApplicationUser entity);
    Task<ApplicationUser> UpdateAsync(ApplicationUser entity);
    Task DeleteAsync(int id);
}
