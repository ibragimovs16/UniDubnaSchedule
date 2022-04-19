using Microsoft.EntityFrameworkCore;
using UniDubnaSchedule.DAL.Interfaces;
using UniDubnaSchedule.Domain.Models;

namespace UniDubnaSchedule.DAL.Repositories;

public class BellsScheduleRepository : IBellsScheduleRepository
{
    private readonly ApplicationDbContext _db;

    public BellsScheduleRepository(ApplicationDbContext db) =>
        _db = db;

    public async Task<List<BellsSchedule>> GetAllAsync() =>
        await _db.BellsSchedule.ToListAsync();

    public async Task<BellsSchedule?> GetByIdAsync(int id) =>
        await _db.BellsSchedule.FirstOrDefaultAsync(bs => bs.PairNumber == id);

    public async Task<int> AddAsync(BellsSchedule data)
    {
        var entity = await _db.BellsSchedule.AddAsync(data);
        await _db.SaveChangesAsync();
        return entity.Entity.PairNumber;
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