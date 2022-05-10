using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UniDubnaSchedule.Domain.DTOs;
using UniDubnaSchedule.Domain.Models;
using UniDubnaSchedule.Services.Abstractions;
using UniDubnaSchedule.Services.Implementations.AuthServices;

namespace UniDubnaSchedule.Backend.Controllers;

[ApiController]
[Route("Api/Auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IConfiguration _configuration;

    public AuthController(IAuthService authService, IConfiguration configuration) =>
        (_authService, _configuration) = (authService, configuration);

    [HttpPost("Register")]
    public async Task<ActionResult> Register([FromBody] UserDto request)
    {
        var result = await _authService.Register(request);
    
        return new ContentResult
        {
            StatusCode = (int) result.StatusCode,
            Content = JsonConvert.SerializeObject(new
            {
                Status = result.StatusCode.ToString(),
                Message = result.Data
            })
        };
    }

    [HttpPost("Login")]
    public async Task<ActionResult> Login([FromBody] UserDto request)
    {
        var result = await _authService.Login(request, _configuration, Response.Cookies);

        return new ContentResult
        {
            StatusCode = (int) result.StatusCode,
            Content = JsonConvert.SerializeObject(new
            {
                Status = result.StatusCode.ToString(),
                AccessToken = result.Data
            })
        };
    }

    [HttpPost("RefreshToken")]
    public async Task<ActionResult> RefreshToken()
    {
        var result = await _authService.RefreshToken(Request.Cookies, Response.Cookies);

        return new ContentResult
        {
            StatusCode = (int) result.StatusCode,
            Content = JsonConvert.SerializeObject(new
            {
                Status = result.StatusCode.ToString(),
                AccessToken = result.Data
            })
        };
    }
}