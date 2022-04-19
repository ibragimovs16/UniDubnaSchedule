using UniDubnaSchedule.Domain.DTOs;
using UniDubnaSchedule.Domain.Models;
using UniDubnaSchedule.Domain.Response;

namespace UniDubnaSchedule.Services.Abstractions;

public interface IScheduleService : IBaseService<JoinedSchedule>
{
    /// <summary>
    /// A service for getting a schedule after joining all tables by group.
    /// </summary>
    /// <returns>An element of the <see cref="BaseResponse{T}" /> class containing data about the execution of a request to the repository</returns>
    Task<BaseResponse<List<JoinedSchedule>>> GetJoinedScheduleByGroupAsync(int group);
    
    /// <summary>
    /// A service for getting a schedule after joining all tables by group and day of week.
    /// </summary>
    /// <returns>An element of the <see cref="BaseResponse{T}" /> class containing data about the execution of a request to the repository</returns>
    Task<BaseResponse<List<JoinedSchedule>>> GetJoinedScheduleByGroupAndWeekDay(int group, int weekDay);
}