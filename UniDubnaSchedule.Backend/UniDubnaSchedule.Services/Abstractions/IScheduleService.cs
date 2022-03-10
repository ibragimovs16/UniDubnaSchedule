using UniDubnaSchedule.Domain.Models;
using UniDubnaSchedule.Domain.Response;

namespace UniDubnaSchedule.Services.Abstractions;

public interface IScheduleService
{
    Task<BaseResponse<List<Schedule>>> GetAllScheduleAsync();
}