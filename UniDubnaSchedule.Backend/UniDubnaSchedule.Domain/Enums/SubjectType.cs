using System.ComponentModel;

namespace UniDubnaSchedule.Domain.Enums;

/// <summary>
/// Enumeration of possible types of subjects.
/// </summary>
public enum SubjectType
{
    [Description("лекция")]
    Lecture = 1,
    [Description("семинар")]
    Seminar
}