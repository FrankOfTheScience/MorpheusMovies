namespace MorpheusMovies.Server.Repository.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<T> GetByNameAsync(string name);
    Task<IEnumerable<T>> GetAllAsync();
}
