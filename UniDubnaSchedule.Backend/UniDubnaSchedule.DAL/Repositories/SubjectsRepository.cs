using Microsoft.EntityFrameworkCore;
using UniDubnaSchedule.DAL.Interfaces;
using UniDubnaSchedule.Domain.Models;

namespace UniDubnaSchedule.DAL.Repositories;

public class SubjectsRepository : ISubjectsRepository
{
    private readonly ApplicationDbContext _db;

    public SubjectsRepository(ApplicationDbContext db) =>
        _db = db;

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
    
    #region Dispose

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    private bool _disposed;

    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _db.Dispose();
            }
        }

        _disposed = true;
    }

    #endregion
}