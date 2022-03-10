using UniDubnaSchedule.Domain.DTOs;
using UniDubnaSchedule.Domain.Models;

namespace UniDubnaSchedule.Services.Extensions;

public static class ScheduleExtensions
{
    public static ScheduleDto AsDto(this Schedule schedule) =>
        new()
        {
            Id = schedule.Id,
            Faculty = schedule.Faculty.ToString(),
            WeekDay = schedule.WeekDay.ToString(),
            PairNumber = schedule.PairNumber,
            Parity = schedule.Parity,
            SubjectType = schedule.SubjectType.ToString(),
            SubjectId = schedule.SubjectId,
            TeacherId = schedule.TeacherId,
            Cabinet = schedule.Cabinet
        };
}