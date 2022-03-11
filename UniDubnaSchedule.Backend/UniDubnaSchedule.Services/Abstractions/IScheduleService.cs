using UniDubnaSchedule.Domain.Models;
using UniDubnaSchedule.Domain.Response;

namespace UniDubnaSchedule.Services.Abstractions;

public interface IScheduleService : IDisposable
{
    Task<BaseResponse<List<JoinedSchedule>>> GetAllJoinedScheduleAsync();
    Task<BaseResponse<List<JoinedSchedule>>> GetJoinedScheduleByGroupAsync(int group);
    Task<BaseResponse<List<JoinedSchedule>>> GetJoinedScheduleByGroupAndWeekDay(int group, int weekDay);
}