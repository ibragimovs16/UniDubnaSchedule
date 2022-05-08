using System.Net;
using Microsoft.Extensions.Configuration;
using UniDubnaSchedule.DAL.Interfaces;
using UniDubnaSchedule.Domain.DTOs;
using UniDubnaSchedule.Domain.Enums;
using UniDubnaSchedule.Domain.Models;
using UniDubnaSchedule.Domain.Response;

namespace UniDubnaSchedule.Services.Implementations.AuthServices;

public class AuthService : IAuthService
{
    private readonly IUsersRepository _repository;
    
    public AuthService(IUsersRepository repository) =>
        _repository = repository;
    
    public async Task<BaseResponse<string>> Register(UserDto user)
    {
        if (await _repository.GetByUsernameAsync(user.Username) is not null)
            return new BaseResponse<string>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Data = "This username is already taken."
            };

        if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
            return new BaseResponse<string>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Data = "The username and password can't be empty."
            };
        
        var (passwordSalt, passwordHash) = await PasswordService.CreatePasswordHash(user.Password);

        var currentUser = new User
        {
            Username = user.Username,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Registered = DateTime.UtcNow,
            Role = Roles.User
        };

        var entity = await _repository.AddAsync(currentUser);
        if (entity != user.Username)
            return new BaseResponse<string>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Data = "Something went wrong, please try again."
            };

        return new BaseResponse<string>
        {
            StatusCode = HttpStatusCode.OK,
            Data = "You have successfully registered."
        };
    }

    public async Task<BaseResponse<string>> Login(UserDto user, IConfiguration configuration)
    {
        var currentUser = await _repository.GetByUsernameAsync(user.Username);

        if (currentUser is null ||
            !await PasswordService.VerifyPassword(
                user.Password,
                new PasswordService.PasswordHashRecord(
                    currentUser.PasswordSalt,
                    currentUser.PasswordHash
                )))
            return new BaseResponse<string>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Data = "Incorrect username or password."
            };
        
        return new BaseResponse<string>
        {
            StatusCode = HttpStatusCode.OK,
            Data = TokenService.CreateToken(currentUser, configuration)
        };
    }
}