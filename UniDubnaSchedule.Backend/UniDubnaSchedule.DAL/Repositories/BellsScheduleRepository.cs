using Microsoft.EntityFrameworkCore;
using UniDubnaSchedule.DAL.Interfaces;
using UniDubnaSchedule.Domain.Models;

namespace UniDubnaSchedule.DAL.Repositories;

public class BellsScheduleRepository : BaseRepository, IBellsScheduleRepository
{ 
    public BellsScheduleRepository(ApplicationDbContext db) : base(db) {}

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
}