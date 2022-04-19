using System.Net;
using UniDubnaSchedule.DAL.Interfaces;
using UniDubnaSchedule.Domain.Models;
using UniDubnaSchedule.Domain.Response;
using UniDubnaSchedule.Services.Abstractions;

namespace UniDubnaSchedule.Services.Implementations;

public class TeacherService : ITeacherService
{
    private readonly ITeachersRepository _repository;

    public TeacherService(ITeachersRepository repository) =>
        _repository = repository;
    
    public async Task<BaseResponse<List<Teacher>>> GetAllAsync()
    {
        var teachers = await _repository.GetAllAsync();
        if (teachers.Count == 0)
            return new BaseResponse<List<Teacher>>
            {
                StatusCode = HttpStatusCode.NotFound,
                Description = "Count = 0."
            };

        return new BaseResponse<List<Teacher>>
        {
            StatusCode = HttpStatusCode.OK,
            Data = teachers
        };
    }

    public async Task<BaseResponse<List<Teacher>>> GetBySurname(string surname)
    {
        var teachers = await _repository.GetBySurnameAsync(surname);
        if (teachers.Count == 0)
            return new BaseResponse<List<Teacher>>
            {
                StatusCode = HttpStatusCode.NotFound,
                Description = "Count = 0."
            };

        return new BaseResponse<List<Teacher>>
        {
            StatusCode = HttpStatusCode.OK,
            Data = teachers
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