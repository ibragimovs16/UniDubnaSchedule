using System.Net;
using UniDubnaSchedule.DAL.Interfaces;
using UniDubnaSchedule.Domain.Models;
using UniDubnaSchedule.Domain.Response;
using UniDubnaSchedule.Services.Abstractions;

namespace UniDubnaSchedule.Services.Implementations;

public class ScheduleService : IScheduleService
{
    private readonly IScheduleRepository _repository;

    public ScheduleService(IScheduleRepository repository) =>
        _repository = repository;

    public async Task<BaseResponse<List<JoinedSchedule>>> GetAllJoinedScheduleAsync()
    {
        var joinedSchedule = await _repository.GetAllJoinedScheduleAsync();
        if (joinedSchedule.Count == 0)
            return new BaseResponse<List<JoinedSchedule>>
            {
                StatusCode = HttpStatusCode.NotFound,
                Description = "Count = 0."
            };

        return new BaseResponse<List<JoinedSchedule>>
        {
            StatusCode = HttpStatusCode.OK,
            Data = joinedSchedule
        };
    }

    public async Task<BaseResponse<List<JoinedSchedule>>> GetJoinedScheduleByGroupAsync(int group)
    {
        var joinedSchedule = await _repository.GetJoinedScheduleByGroupAsync(group);

        if (joinedSchedule.Count == 0)
            return new BaseResponse<List<JoinedSchedule>>
            {
                StatusCode = HttpStatusCode.NotFound,
                Description = "The group number is entered incorrectly or is missing from the database."
            };

        return new BaseResponse<List<JoinedSchedule>>
        {
            StatusCode = HttpStatusCode.OK,
            Data = joinedSchedule
        };
    }

    public async Task<BaseResponse<List<JoinedSchedule>>> GetJoinedScheduleByGroupAndWeekDay(int group, int weekDay)
    {
        var joinedSchedule = 
            await _repository.GetJoinedScheduleByGroupAndWeekDayAsync(group, weekDay);

        if (joinedSchedule.Count == 0)
            return new BaseResponse<List<JoinedSchedule>>
            {
                StatusCode = HttpStatusCode.NotFound,
                Description = 
                    "The group number or day of the week is entered incorrectly or is missing from the database."
            };

        return new BaseResponse<List<JoinedSchedule>>
        {
            StatusCode = HttpStatusCode.OK,
            Data = joinedSchedule
        };
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
                _repository.Dispose();
            }
        }

        _disposed = true;
    }

    #endregion
}