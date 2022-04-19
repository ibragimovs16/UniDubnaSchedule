using Microsoft.EntityFrameworkCore;
using UniDubnaSchedule.DAL.Interfaces;
using UniDubnaSchedule.Domain.Models;
using UniDubnaSchedule.Domain.Response;

namespace UniDubnaSchedule.DAL.Repositories;

public class TeacherRepository : ITeachersRepository
{
    private readonly ApplicationDbContext _db;

    public TeacherRepository(ApplicationDbContext db) =>
        _db = db;

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