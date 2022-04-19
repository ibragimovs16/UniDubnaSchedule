using UniDubnaSchedule.Domain.Models;
using UniDubnaSchedule.Domain.Response;

namespace UniDubnaSchedule.DAL.Interfaces;

public interface ITeachersRepository : IBaseRepository<Teacher>
{
    Task<List<Teacher>> GetBySurnameAsync(string surname);
}