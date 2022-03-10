namespace UniDubnaSchedule.DAL.Interfaces;

public interface IBaseRepository<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetById(int id);
}