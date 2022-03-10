using System.ComponentModel;

namespace UniDubnaSchedule.Domain.Enums;

public enum SubjectType
{
    [Description("лекция")]
    Lecture = 1,
    [Description("семинар")]
    Seminar
}