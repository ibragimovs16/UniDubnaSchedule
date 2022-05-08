using System.Net;
using Microsoft.AspNetCore.Mvc;
using UniDubnaSchedule.Domain.Models;
using UniDubnaSchedule.Services.Abstractions;

namespace UniDubnaSchedule.Backend.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class SubjectsController : ControllerBase
{
    private readonly ISubjectsService _subjects;

    public SubjectsController(ISubjectsService subjects) =>
        _subjects = subjects;

    [HttpGet]
    public async Task<ActionResult<List<Subject>>> GetAllSubjectsAsync()
    {
        var subjectsResponse = await _subjects.GetAllAsync();

        if (subjectsResponse.StatusCode == HttpStatusCode.NotFound)
            return NotFound(subjectsResponse.Description);

        return Ok(subjectsResponse.Data);
    }
}