using Microsoft.EntityFrameworkCore;
using UniDubnaSchedule.DAL.Interfaces;
using UniDubnaSchedule.Domain.Models;

namespace UniDubnaSchedule.DAL.Repositories;

public class ScheduleRepository : IScheduleRepository
{
    private readonly ApplicationDbContext _db;

    public ScheduleRepository(ApplicationDbContext db) =>
        _db = db;

    public async Task<List<Schedule>> GetAll() =>
        await _db.Schedule.ToListAsync();

    public async Task<Schedule?> GetById(int id) =>
        await _db.Schedule.FirstOrDefaultAsync(sch => sch.Id == id);

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