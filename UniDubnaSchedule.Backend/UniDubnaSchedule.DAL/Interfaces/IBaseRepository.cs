namespace UniDubnaSchedule.DAL.Interfaces;

public interface IBaseRepository<T> : IDisposable
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
}