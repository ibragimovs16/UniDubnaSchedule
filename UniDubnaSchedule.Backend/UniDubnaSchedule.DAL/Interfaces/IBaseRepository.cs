namespace UniDubnaSchedule.DAL.Interfaces;

public interface IBaseRepository<T> : IDisposable
{
    Task<List<T>> GetAll();
    Task<T?> GetById(int id);
}