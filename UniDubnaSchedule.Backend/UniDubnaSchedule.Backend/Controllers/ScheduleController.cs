using System.Net;
using Microsoft.AspNetCore.Mvc;
using UniDubnaSchedule.Domain.DTOs;
using UniDubnaSchedule.Domain.Models;
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
    public async Task<ActionResult<List<JoinedScheduleDto>>> GetAllScheduleAsync()
    {
        var scheduleResponse = await _schedule.GetAllJoinedScheduleAsync();

        if (scheduleResponse.StatusCode == HttpStatusCode.NotFound)
            return NotFound(scheduleResponse.Description);

        return Ok(scheduleResponse.Data?.Select(item => item.AsDto()));
    }

    [HttpGet("{group:int}")]
    public async Task<ActionResult<List<JoinedScheduleDto>>> GetScheduleByGroupAsync(int group)
    {
        var scheduleResponse = await _schedule.GetJoinedScheduleByGroupAsync(group);
    
        if (scheduleResponse.StatusCode == HttpStatusCode.NotFound)
            return NotFound(scheduleResponse.Description);
    
        return Ok(scheduleResponse.Data?.Select(item => item.AsDto()));
    }

    [HttpGet("{group:int}/{weekDay:int}")]
    public async Task<ActionResult<List<JoinedSchedule>>> GetScheduleByGroupAndWeekDayAsync(int group, int weekDay)
    {
        var scheduleResponse = await _schedule.GetJoinedScheduleByGroupAndWeekDay(group, weekDay);

        if (scheduleResponse.StatusCode == HttpStatusCode.NotFound)
            return NotFound(scheduleResponse.Description);
        
        return Ok(scheduleResponse.Data?.Select(item => item.AsDto()));
    }
}