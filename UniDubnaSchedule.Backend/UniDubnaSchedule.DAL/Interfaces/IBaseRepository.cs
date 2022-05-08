namespace UniDubnaSchedule.DAL.Interfaces;

// Basic interface for repositories.
public interface IBaseRepository<T> : IDisposable
{
    /// <summary>
    /// Get all records from the database.
    /// </summary>
    /// <returns>A collection with all records</returns>
    Task<List<T>> GetAllAsync();
}