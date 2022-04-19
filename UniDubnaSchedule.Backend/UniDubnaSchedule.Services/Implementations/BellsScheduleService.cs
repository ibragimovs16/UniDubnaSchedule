using System.Net;
using UniDubnaSchedule.DAL.Interfaces;
using UniDubnaSchedule.Domain.Models;
using UniDubnaSchedule.Domain.Response;
using UniDubnaSchedule.Services.Abstractions;

namespace UniDubnaSchedule.Services.Implementations;

public class BellsScheduleService : IBellsScheduleService
{
    private readonly IBellsScheduleRepository _repository;

    public BellsScheduleService(IBellsScheduleRepository repository) =>
        _repository = repository;
    
    public async Task<BaseResponse<List<BellsSchedule>>> GetAllAsync()
    {
        var bellsSchedule = await _repository.GetAllAsync();
        if (bellsSchedule.Count == 0)
            return new BaseResponse<List<BellsSchedule>>
            {
                StatusCode = HttpStatusCode.NotFound,
                Description = "Count = 0."
            };

        return new BaseResponse<List<BellsSchedule>>
        {
            StatusCode = HttpStatusCode.OK,
            Data = bellsSchedule
        };
    }
    
    public async Task<BaseResponse<BellsSchedule>> GetByPairNumber(int pairNumber)
    {
        var bellsSchedule = await _repository.GetByIdAsync(pairNumber);
        if (bellsSchedule is null)
            return new BaseResponse<BellsSchedule>
            {
                StatusCode = HttpStatusCode.NotFound,
                Description = "The element was not found."
            };

        return new BaseResponse<BellsSchedule>
        {
            StatusCode = HttpStatusCode.OK,
            Data = bellsSchedule
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