using UniDubnaSchedule.Domain.Models;

namespace UniDubnaSchedule.DAL.Interfaces;

public interface IScheduleRepository : IBaseRepository<Schedule>
{
    Task<List<JoinedSchedule>> GetAllJoinedScheduleAsync();
    Task<bool> IsGroupExists(int group);
    Task<List<JoinedSchedule>> GetJoinedScheduleByGroupAsync(int group);
    Task<List<JoinedSchedule>> GetJoinedScheduleByGroupAndWeekDayAsync(int group, int weekDay);
}