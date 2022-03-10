namespace UniDubnaSchedule.Domain.Models;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Abbreviation { get; set; }
}