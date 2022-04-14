namespace UniDubnaSchedule.Domain.Models;

/// <summary>
/// Model for the Subject table.
/// </summary>
public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Abbreviation { get; set; }
}