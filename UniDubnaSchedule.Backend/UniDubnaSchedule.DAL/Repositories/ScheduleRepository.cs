using System.Globalization;
using Microsoft.EntityFrameworkCore;
using UniDubnaSchedule.DAL.Interfaces;
using UniDubnaSchedule.Domain.Models;

namespace UniDubnaSchedule.DAL.Repositories;

public class ScheduleRepository : IScheduleRepository
{
    private readonly ApplicationDbContext _db;

    public ScheduleRepository(ApplicationDbContext db) =>
        _db = db;

    public async Task<List<Schedule>> GetAllAsync() =>
        await _db.Schedule.ToListAsync();

    public async Task<Schedule?> GetByIdAsync(int id) =>
        await _db.Schedule.FirstOrDefaultAsync(sch => sch.Id == id);

    public async Task<int> AddAsync(Schedule data)
    {
        var entity = await _db.Schedule.AddAsync(data);
        await _db.SaveChangesAsync();
        return entity.Entity.Id;
    }

    public async Task<bool> IsGroupExists(int group) =>
        await _db.Schedule.AnyAsync(s => s.Group == group);
    
    public async Task<List<JoinedSchedule>> GetAllJoinedScheduleAsync()
    {
        var rawSchedule = _db.Schedule.AsQueryable();
    
        if ((await rawSchedule.ToListAsync()).Count == 0) 
            return new List<JoinedSchedule>();
    
        return await JoinSchedule(rawSchedule).ToListAsync();
    }

    public async Task<List<JoinedSchedule>> GetJoinedScheduleByGroupAsync(int group)
    {
        if (!await IsGroupExists(group))
            return new List<JoinedSchedule>();
        
        var rawSchedule = await SelectByGroupRaw(group);

        if ((await rawSchedule.ToListAsync()).Count == 0)
            return new List<JoinedSchedule>();

        return await JoinSchedule(rawSchedule).ToListAsync();
    }

    public async Task<List<JoinedSchedule>> GetJoinedScheduleByGroupAndWeekDayAsync(int group, int weekDay)
    {
        if (!await IsGroupExists(group) || !IsWeekDayCorrect(weekDay))
            return new List<JoinedSchedule>();

        var rawSchedule = await SelectByGroupAndWeekDayRaw(group, weekDay);

        if ((await rawSchedule.ToListAsync()).Count == 0)
            return new List<JoinedSchedule>();

        return await JoinSchedule(rawSchedule).ToListAsync();
    }
    
    /// <summary>
    /// Check the correctness of the day of the week.
    /// </summary>
    /// <param name="weekDay">Week day</param>
    private bool IsWeekDayCorrect(int weekDay) =>
        1 <= weekDay && weekDay <= 7;
    
    /// <summary>
    /// Calculate the parity of the week.
    /// </summary>
    /// <returns>0 if the week is even, 1 if the week is odd</returns>
    private int WeekDayParity() =>
        (ISOWeek.GetWeekOfYear(DateTimeOffset.UtcNow.LocalDateTime) - 1) % 2;
    
    /// <summary>
    /// Get all records by group number from the database.
    /// </summary>
    /// <param name="group">Group number</param>
    /// <returns>Collection of records</returns>
    private async Task<IQueryable<Schedule>> SelectByGroupRaw(int group) =>
        await Task.FromResult(
            _db.Schedule
                .Where(s => s.Group == group)
                .Where(s => s.Parity == -1 || s.Parity == WeekDayParity())
        );
    
    /// <summary>
    /// Get all records by group number and day of weak from the database.
    /// </summary>
    /// <param name="group">Group number</param>
    /// <param name="weekDay">day of week</param>
    /// <returns>Collection of records</returns>
    private async Task<IQueryable<Schedule>> SelectByGroupAndWeekDayRaw(int group, int weekDay) =>
        await Task.FromResult(
            _db.Schedule
                .Where(s => s.Group == group)
                .Where(s => s.Parity == -1 || s.Parity == WeekDayParity())
                .Where(s => (int)s.WeekDay == weekDay)
        );
    
    /// <summary>
    /// Combines several tables from the database into one record.
    /// </summary>
    /// <param name="schedule">Raw schedule received from the database</param>
    /// <returns>Record with type <see cref="JoinedSchedule" /></returns>
    private IQueryable<JoinedSchedule> JoinSchedule(IQueryable<Schedule> schedule) =>
        from sch in schedule
        join t in _db.Teachers on sch.TeacherId equals t.Id
        join sub in _db.Subjects on sch.SubjectId equals sub.Id
        join bs in _db.BellsSchedule on sch.PairNumber equals bs.PairNumber
        select new JoinedSchedule
        {
            Faculty = sch.Faculty,
            Group = sch.Group,
            WeekDay = sch.WeekDay,
            PairNumber = sch.PairNumber,
            Parity = sch.Parity,
            SubjectType = sch.SubjectType,
            SubjectName = sub.Name,
            SubjectAbbreviation = sub.Abbreviation,
            TeacherSurname = t.Surname,
            TeacherName = t.Name,
            TeacherMiddleName = t.MiddleName,
            TeacherEmail = t.Email,
            Cabinet = sch.Cabinet,
            StartTime = bs.Start,
            EndTime = bs.End
        };

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