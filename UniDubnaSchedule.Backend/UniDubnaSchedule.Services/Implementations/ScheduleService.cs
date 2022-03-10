using UniDubnaSchedule.DAL.Interfaces;
using UniDubnaSchedule.Domain.Enums;
using UniDubnaSchedule.Domain.Models;
using UniDubnaSchedule.Domain.Response;
using UniDubnaSchedule.Services.Abstractions;

namespace UniDubnaSchedule.Services.Implementations;

public class ScheduleService : IScheduleService
{
    private readonly IScheduleRepository _repository;

    public ScheduleService(IScheduleRepository repository) =>
        _repository = repository;
    
    public async Task<BaseResponse<List<Schedule>>> GetAllScheduleAsync()
    {
        var schedule = await _repository.GetAll();
        if (!schedule.Any())
            return new BaseResponse<List<Schedule>>
            {
                StatusCode = StatusCode.NotFound,
                Description = "Count = 0"
            };

        return new BaseResponse<List<Schedule>>
        {
            StatusCode = StatusCode.Ok,
            Data = schedule
        };
    }
}