using Microsoft.AspNetCore.Mvc;
using UniDubnaSchedule.Domain.DTOs;
using UniDubnaSchedule.Services.Abstractions;
using UniDubnaSchedule.Services.Extensions;

namespace UniDubnaSchedule.Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class ScheduleController : ControllerBase
{
    private readonly IScheduleService _schedule;

    public ScheduleController(IScheduleService schedule) =>
        _schedule = schedule;

    [HttpGet]
    public async Task<ActionResult<List<ScheduleDto>>> GetAllScheduleAsync()
    {
        var scheduleResponse = await _schedule.GetAllScheduleAsync();

        if (scheduleResponse.StatusCode == Domain.Enums.StatusCode.NotFound)
            return NotFound();

        return Ok(scheduleResponse.Data?.Select(item => item.AsDto()));
    }
}