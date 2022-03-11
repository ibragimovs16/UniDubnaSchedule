using UniDubnaSchedule.Domain.Enums;

namespace UniDubnaSchedule.Domain.Models;

public class JoinedSchedule
{
    public Faculty Faculty { get; set; }
    public int Group { get; set; }
    public WeekDay WeekDay { get; set; }
    public int Parity { get; set; }
    public int PairNumber { get; set; }
    public SubjectType SubjectType { get; set; }
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