using UniDubnaSchedule.Domain.Enums;
using UniDubnaSchedule.Domain.Models;

namespace UniDubnaSchedule.DAL.Interfaces;

public interface IUsersRepository : IBaseRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByRefreshTokenAsync(string refreshToken);
    Task<string> UpdateAsync(User data);
    Task<string> AddAsync(User data);
    Task RemoveAsync(User data);
}