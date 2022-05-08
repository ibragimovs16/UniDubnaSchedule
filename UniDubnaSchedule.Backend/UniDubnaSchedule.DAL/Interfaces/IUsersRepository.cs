using UniDubnaSchedule.Domain.Models;

namespace UniDubnaSchedule.DAL.Interfaces;

public interface IUsersRepository : IBaseRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);
    Task<string> AddAsync(User data);
    Task RemoveAsync(User data);
}