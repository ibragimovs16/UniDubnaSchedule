namespace UniDubnaSchedule.Domain.Models;

/// <summary>
/// Model for the Teacher table.
/// </summary>
public class Teacher
{
    public int Id { get; set; }
    public string Surname { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string? Email { get; set; }
}