using System.Net;
using UniDubnaSchedule.DAL.Interfaces;
using UniDubnaSchedule.Domain.Models;
using UniDubnaSchedule.Domain.Response;
using UniDubnaSchedule.Services.Abstractions;

namespace UniDubnaSchedule.Services.Implementations;

public class SubjectsService : ISubjectsService
{
    private readonly ISubjectsRepository _repository;

    public SubjectsService(ISubjectsRepository repository) =>
        _repository = repository;
    
    public async Task<BaseResponse<List<Subject>>> GetAllAsync()
    {
        var subjects = await _repository.GetAllAsync();
        if (subjects.Count == 0)
            return new BaseResponse<List<Subject>>
            {
                StatusCode = HttpStatusCode.NotFound,
                Description = "Count = 0."
            };

        return new BaseResponse<List<Subject>>
        {
            StatusCode = HttpStatusCode.OK,
            Data = subjects
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