using Microsoft.EntityFrameworkCore;
using UniDubnaSchedule.DAL.Interfaces;
using UniDubnaSchedule.Domain.Models;

namespace UniDubnaSchedule.DAL.Repositories;

public class SubjectsRepository : BaseRepository, ISubjectsRepository
{
    public SubjectsRepository(ApplicationDbContext db) : base(db) {}

    public async Task<List<Subject>> GetAllAsync() =>
        await _db.Subjects.ToListAsync();

    public async Task<Subject?> GetByIdAsync(int id) =>
        await _db.Subjects.FirstOrDefaultAsync(s => s.Id == id);

    public async Task<int> AddAsync(Subject data)
    {
        var entity = await _db.Subjects.AddAsync(data);
        await _db.SaveChangesAsync();
        return entity.Entity.Id;
    }
}