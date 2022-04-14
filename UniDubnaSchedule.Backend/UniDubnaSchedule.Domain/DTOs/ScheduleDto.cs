namespace UniDubnaSchedule.Domain.DTOs;

/// <summary>
/// Basic schedule model
/// </summary>
public class ScheduleDto
{
    public int Id { get; set; }
    public string Faculty { get; set; } = string.Empty;
    public string FacultyRu { get; set; } = string.Empty;
    public int Group { get; set; }
    public string WeekDay { get; set; } = string.Empty;
    public string WeekDayRu { get; set; } = string.Empty;
    public int PairNumber { get; set; }
    public int Parity { get; set; }
    public string SubjectType { get; set; } = string.Empty;
    public string SubjectTypeRu { get; set; } = string.Empty;
    public int SubjectId { get; set; }
    public int TeacherId { get; set; }
    public string Cabinet { get; set; } = string.Empty;
}