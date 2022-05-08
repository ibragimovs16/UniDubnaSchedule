using System.Net;
using Microsoft.AspNetCore.Mvc;
using UniDubnaSchedule.Domain.Models;
using UniDubnaSchedule.Services.Abstractions;

namespace UniDubnaSchedule.Backend.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class TeachersController : ControllerBase
{
    private readonly ITeacherService _teachers;

    public TeachersController(ITeacherService teachers) =>
        _teachers = teachers;

    [HttpGet]
    public async Task<ActionResult<List<Teacher>>> GetAllTeachersAsync()
    {
        var teachersResponse = await _teachers.GetAllAsync();
    
        if (teachersResponse.StatusCode == HttpStatusCode.NotFound)
            return NotFound(teachersResponse.Description);
    
        return Ok(teachersResponse.Data);
    }

    [HttpGet("{surname}")]
    public async Task<ActionResult<List<Teacher>>> GetTeacherBySurnameAsync(string surname)
    {
        var teachersResponse = await _teachers.GetBySurname(surname);

        if (teachersResponse.StatusCode == HttpStatusCode.NotFound)
            return NotFound(teachersResponse.Description);

        return Ok(teachersResponse.Data);
    }
}