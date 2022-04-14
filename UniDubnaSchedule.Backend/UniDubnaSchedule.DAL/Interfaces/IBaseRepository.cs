namespace UniDubnaSchedule.DAL.Interfaces;

// Basic interface for repositories.
public interface IBaseRepository<T> : IDisposable
{
    /// <summary>
    /// Get all records from the database.
    /// </summary>
    /// <returns>A collection with all records</returns>
    Task<List<T>> GetAllAsync();
    /// <summary>
    /// Get record by id from the database.
    /// </summary>
    /// <param name="id">Record id</param>
    /// <returns>Null or the resulting element</returns>
    Task<T?> GetByIdAsync(int id);
}