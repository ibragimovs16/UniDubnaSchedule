using System.Net;
using Microsoft.AspNetCore.Mvc;
using UniDubnaSchedule.Domain.Models;
using UniDubnaSchedule.Services.Abstractions;

namespace UniDubnaSchedule.Backend.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class BellsScheduleController : ControllerBase
{
    private readonly IBellsScheduleService _bellsSchedule;

    public BellsScheduleController(IBellsScheduleService bellsSchedule) =>
        _bellsSchedule = bellsSchedule;

    [HttpGet]
    public async Task<ActionResult<List<BellsSchedule>>> GetAllBellsScheduleAsync()
    {
        var bellsScheduleResponse = await _bellsSchedule.GetAllAsync();

        if (bellsScheduleResponse.StatusCode == HttpStatusCode.NotFound)
            return NotFound(bellsScheduleResponse.Description);

        return Ok(bellsScheduleResponse.Data);
    }

    [HttpGet("{pairNumber:int}")]
    public async Task<ActionResult<BellsSchedule>> GetBellsScheduleByPairNumberAsync(int pairNumber)
    {
        var bellsScheduleResponse = await _bellsSchedule.GetByPairNumber(pairNumber);

        if (bellsScheduleResponse.StatusCode == HttpStatusCode.NotFound)
            return NotFound(bellsScheduleResponse.Description);

        return Ok(bellsScheduleResponse.Data);
    }
}