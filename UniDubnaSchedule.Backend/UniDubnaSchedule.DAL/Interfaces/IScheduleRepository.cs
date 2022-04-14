using UniDubnaSchedule.Domain.Models;

namespace UniDubnaSchedule.DAL.Interfaces;

public interface IScheduleRepository : IBaseRepository<Schedule>
{
    /// <summary>
    /// Checking for records with the specified group in the database.
    /// </summary>
    /// <param name="group">Group number</param>
    Task<bool> IsGroupExists(int group);
    /// <summary>
    /// Get all the records after from the database, joins multiple tables.
    /// </summary>
    /// <returns>Collection of records with type <see cref="JoinedSchedule" /></returns>
    Task<List<JoinedSchedule>> GetAllJoinedScheduleAsync();
    /// <summary>
    /// Get records by id after from the database, joins multiple tables.
    /// </summary>
    /// <param name="group">Group number</param>
    /// <returns>Collection of records with type <see cref="JoinedSchedule" /></returns>
    Task<List<JoinedSchedule>> GetJoinedScheduleByGroupAsync(int group);
    /// <summary>
    /// Get records by id and week day after from the database, joins multiple tables.
    /// </summary>
    /// <param name="group">Group number</param>
    /// <param name="weekDay">Week day</param>
    /// <returns>Collection of records with type <see cref="JoinedSchedule" /></returns>
    Task<List<JoinedSchedule>> GetJoinedScheduleByGroupAndWeekDayAsync(int group, int weekDay);
}