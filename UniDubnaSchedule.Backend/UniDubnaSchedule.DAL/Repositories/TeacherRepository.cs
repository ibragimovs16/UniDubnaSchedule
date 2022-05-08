using Microsoft.EntityFrameworkCore;
using UniDubnaSchedule.DAL.Interfaces;
using UniDubnaSchedule.Domain.Models;

namespace UniDubnaSchedule.DAL.Repositories;

public class TeacherRepository : BaseRepository, ITeachersRepository
{
    public TeacherRepository(ApplicationDbContext db) : base(db) {}

    public async Task<List<Teacher>> GetAllAsync() =>
        await _db.Teachers.ToListAsync();

    public async Task<Teacher?> GetByIdAsync(int id) =>
        await _db.Teachers.FirstOrDefaultAsync(t => t.Id == id);

    public async Task<List<Teacher>> GetBySurnameAsync(string surname) =>
        await _db.Teachers.Where(t => t.Surname == surname).ToListAsync();

    public async Task<int> AddAsync(Teacher data)
    {
        var entity = await _db.Teachers.AddAsync(data);
        await _db.SaveChangesAsync();
        return entity.Entity.Id;
    }
}