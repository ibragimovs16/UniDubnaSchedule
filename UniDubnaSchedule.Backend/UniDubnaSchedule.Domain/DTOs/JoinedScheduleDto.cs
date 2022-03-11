namespace UniDubnaSchedule.Domain.DTOs;

public class JoinedScheduleDto
{
    public string Faculty { get; set; } = string.Empty;
    public string FacultyRu { get; set; } = string.Empty;
    public int Group { get; set; }
    public string WeekDay { get; set; } = string.Empty;
    public string WeekDayRu { get; set; } = string.Empty;
    public int Parity { get; set; }
    public int PairNumber { get; set; }
    public string SubjectType { get; set; } = string.Empty;
    public string SubjectTypeRu { get; set; } = string.Empty;
    public string SubjectName { get; set; } = string.Empty;
    public string? SubjectAbbreviation { get; set; }
    public string TeacherSurname { get; set; } = string.Empty;
    public string TeacherName { get; set; } = string.Empty;
    public string? TeacherMiddleName { get; set; }
    public string? TeacherEmail { get; set; }
    public string Cabinet { get; set; } = string.Empty;
    public string StartTime { get; set; } = string.Empty;
    public string EndTime { get; set; } = string.Empty;
}