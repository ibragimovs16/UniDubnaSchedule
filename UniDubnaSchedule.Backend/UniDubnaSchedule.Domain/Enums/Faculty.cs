using System.ComponentModel;

namespace UniDubnaSchedule.Domain.Enums;

public enum Faculty
{
    [Description("ИСАУ")]
    ISAM = 1,
    [Description("ФЕИН")]
    FNES,
    [Description("ИФИ")]
    EPI,
    [Description("ФСГН")]
    FSHS
}