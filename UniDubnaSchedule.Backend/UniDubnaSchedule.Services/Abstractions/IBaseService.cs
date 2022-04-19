using UniDubnaSchedule.Domain.Response;

namespace UniDubnaSchedule.Services.Abstractions;

public interface IBaseService<T> : IDisposable
{
    /// <summary>
    /// A service for getting all the data using the repository.
    /// </summary>
    /// <returns>An element containing data about the execution of a request to the repository</returns>
    Task<BaseResponse<List<T>>> GetAllAsync();
}