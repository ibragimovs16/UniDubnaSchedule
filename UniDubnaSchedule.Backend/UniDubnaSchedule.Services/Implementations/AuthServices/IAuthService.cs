using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using UniDubnaSchedule.Domain.DTOs;
using UniDubnaSchedule.Domain.Response;

namespace UniDubnaSchedule.Services.Implementations.AuthServices;

public interface IAuthService
{
    Task<BaseResponse<string>> Register(UserDto user);
    Task<BaseResponse<string>> Login(UserDto user, IConfiguration configuration, IResponseCookies responseCookies);
    Task<BaseResponse<string>> RefreshToken(IRequestCookieCollection requestCookies, IResponseCookies responseCookies);
}