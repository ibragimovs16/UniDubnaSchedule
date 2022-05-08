using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UniDubnaSchedule.Domain.DTOs;
using UniDubnaSchedule.Services.Abstractions;

namespace UniDubnaSchedule.Backend.Controllers;

[ApiController]
[Route("Api/Auth")]
public class AuthController : ControllerBase
{
    private readonly IUsersService _usersService;

    public AuthController(IUsersService usersService) =>
        _usersService = usersService;

    [HttpPost("Register")]
    public async Task<ActionResult> Register([FromBody] UserDto request)
    {
        var result = await _usersService.Register(request);
    
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
}