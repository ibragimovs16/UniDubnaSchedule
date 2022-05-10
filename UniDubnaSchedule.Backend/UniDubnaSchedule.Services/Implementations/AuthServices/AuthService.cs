using System.Net;
using Microsoft.AspNetCore.Http;
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
    private readonly IConfiguration _configuration;

    public AuthService(IUsersRepository repository, IConfiguration configuration) =>
        (_repository, _configuration) = (repository, configuration);
    
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

    public async Task<BaseResponse<string>> Login(UserDto user, IConfiguration configuration, 
        IResponseCookies responseCookies)
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
        
        var newRefreshToken = TokenService.GenerateRefreshToken(_configuration);
        await SetRefreshToken(currentUser, newRefreshToken, responseCookies);
        
        return new BaseResponse<string>
        {
            StatusCode = HttpStatusCode.OK,
            Data = TokenService.CreateToken(currentUser, configuration)
        };
    }

    public async Task<BaseResponse<string>> RefreshToken(IRequestCookieCollection requestCookies, 
        IResponseCookies responseCookies)
    {
        var refreshToken = requestCookies["refreshToken"];
        var currentUser = await _repository.GetByRefreshTokenAsync(refreshToken ?? string.Empty);

        if (currentUser is null)
            return new BaseResponse<string>
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Data = "Invalid refresh token."
            };

        if (currentUser.RefreshTokenExpires < DateTime.UtcNow)
            return new BaseResponse<string>
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Data = "Refresh token expired."
            };

        var token = TokenService.CreateToken(currentUser, _configuration);
        var newRefreshToken = TokenService.GenerateRefreshToken(_configuration);
        await SetRefreshToken(currentUser, newRefreshToken, responseCookies);

        return new BaseResponse<string>
        {
            StatusCode = HttpStatusCode.OK,
            Data = token
        };
    }

    private async Task SetRefreshToken(User currentUser, RefreshToken refreshToken, IResponseCookies responseCookies)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = refreshToken.Expires
        };
        
        responseCookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        currentUser.RefreshToken = refreshToken.Token;
        currentUser.RefreshTokenCreated = refreshToken.Created;
        currentUser.RefreshTokenExpires = refreshToken.Expires;

        await _repository.UpdateAsync(currentUser);
    }
}