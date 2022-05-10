using Microsoft.EntityFrameworkCore;
using UniDubnaSchedule.DAL.Interfaces;
using UniDubnaSchedule.Domain.Enums;
using UniDubnaSchedule.Domain.Models;

namespace UniDubnaSchedule.DAL.Repositories;

public class UsersRepository : BaseRepository, IUsersRepository
{
    public UsersRepository(ApplicationDbContext db) : base(db) {}

    public async Task<List<User>> GetAllAsync() =>
        await _db.Users.ToListAsync();


    public async Task<User?> GetByUsernameAsync(string username) =>
        await _db.Users.FirstOrDefaultAsync(user => user.Username == username);

    public async Task<User?> GetByRefreshTokenAsync(string refreshToken) =>
        await _db.Users.FirstOrDefaultAsync(user => user.RefreshToken == refreshToken);

    public async Task<string> UpdateAsync(User data)
    {
        var entity = _db.Users.Update(data);
        await _db.SaveChangesAsync();
        return entity.Entity.Username;
    }

    public async Task<string> AddAsync(User data)
    {
        var entity = await _db.Users.AddAsync(data);
        await _db.SaveChangesAsync();
        return entity.Entity.Username;
    }

    public async Task RemoveAsync(User data)
    {
        _db.Users.Remove(data);
        await _db.SaveChangesAsync();
    }
}