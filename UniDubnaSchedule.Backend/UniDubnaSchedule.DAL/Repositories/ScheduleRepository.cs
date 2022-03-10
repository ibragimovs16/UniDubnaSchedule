using Microsoft.EntityFrameworkCore;
using UniDubnaSchedule.DAL.Interfaces;
using UniDubnaSchedule.Domain.Models;

namespace UniDubnaSchedule.DAL.Repositories;

public class ScheduleRepository : IScheduleRepository
{
    private readonly ApplicationDbContext _db;

    public ScheduleRepository(ApplicationDbContext db) =>
        _db = db;

    public async Task<IEnumerable<Schedule>> GetAll() =>
        await _db.Schedule.ToListAsync();

    public async Task<Schedule?> GetById(int id) =>
        await _db.Schedule.FirstOrDefaultAsync(sch => sch.Id == id);
}