using System.Net;
using Microsoft.Extensions.Configuration;
using UniDubnaSchedule.DAL.Interfaces;
using UniDubnaSchedule.Domain.DTOs;
using UniDubnaSchedule.Domain.Enums;
using UniDubnaSchedule.Domain.Models;
using UniDubnaSchedule.Domain.Response;
using UniDubnaSchedule.Services.Abstractions;
using UniDubnaSchedule.Services.Implementations.AuthServices;

namespace UniDubnaSchedule.Services.Implementations;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _repository;
    
    public UsersService(IUsersRepository repository) =>
        _repository = repository;
    
    public async Task<BaseResponse<List<User>>> GetAllAsync()
    {
        var users = await _repository.GetAllAsync();
        if (users.Count == 0)
            return new BaseResponse<List<User>>
            {
                StatusCode = HttpStatusCode.NotFound,
                Description = "Count = 0"
            };

        return new BaseResponse<List<User>>
        {
            StatusCode = HttpStatusCode.OK,
            Data = users
        };
    }

    public async Task<BaseResponse<string>> RemoveUser(string username)
    {
        var currentUser = await _repository.GetByUsernameAsync(username);

        if (currentUser is null)
            return new BaseResponse<string>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Data = "User not found."
            };

        await _repository.RemoveAsync(currentUser);

        return new BaseResponse<string>
        {
            StatusCode = HttpStatusCode.OK,
            Data = "The user has been successfully deleted."
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