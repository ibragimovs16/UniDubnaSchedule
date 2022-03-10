using System.ComponentModel;

namespace UniDubnaSchedule.Domain.Enums;

public enum WeekDay
{
    [Description("Понедельник")]
    Monday = 1,
    [Description("Вторник")]
    Tuesday,
    [Description("Среда")]
    Wednesday,
    [Description("Четверг")]
    Thursday,
    [Description("Пятница")]
    Friday,
    [Description("Суббота")]
    Saturday,
    [Description("Воскресеньк")]
    Sunday
}