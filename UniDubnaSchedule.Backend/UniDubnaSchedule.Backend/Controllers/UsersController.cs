using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UniDubnaSchedule.Services.Abstractions;

namespace UniDubnaSchedule.Backend.Controllers;

[ApiController]
[Route("Api/[controller]")]
[Authorize(Roles = "Admin")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService) =>
        _usersService = usersService;

    [HttpGet]
    public async Task<ActionResult> GetAllUsers()
    {
        var response = await _usersService.GetAllAsync();

        return new ContentResult
        {
            StatusCode = (int) response.StatusCode,
            Content = JsonConvert.SerializeObject(new
            {
                Status = response.StatusCode.ToString(),
                Result = response.Data
            }),
            ContentType = "application/json"
        };
    }

    [HttpDelete]
    public async Task<ActionResult> RemoveUser([FromBody] string username)
    {
        var response = await _usersService.RemoveUser(username);

        return new ContentResult
        {
            StatusCode = (int) response.StatusCode,
            Content = JsonConvert.SerializeObject(new
            {
                Status = response.StatusCode.ToString(),
                Result = response.Data
            })
        };
    }
}