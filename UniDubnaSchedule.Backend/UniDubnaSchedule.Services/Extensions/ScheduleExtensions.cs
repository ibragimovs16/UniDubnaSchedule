using UniDubnaSchedule.DAL.Repositories;
using UniDubnaSchedule.Domain.DTOs;
using UniDubnaSchedule.Domain.Enums;
using UniDubnaSchedule.Domain.Models;

namespace UniDubnaSchedule.Services.Extensions;

public static class ScheduleExtensions
{
    /// <summary>
    /// Extension for converting the model to dto.
    /// </summary>
    /// <param name="schedule"><see cref="Schedule" /> type model</param>
    /// <returns><see cref="ScheduleDto" /> type model</returns>
    public static ScheduleDto AsDto(this Schedule schedule) =>
        new()
        {
            Id = schedule.Id,
            Faculty = schedule.Faculty.ToString(),
            FacultyRu = AttributeRepository<Faculty>.GetDescription(schedule.Faculty),
            Group = schedule.Group,
            WeekDay = schedule.WeekDay.ToString(),
            WeekDayRu = AttributeRepository<WeekDay>.GetDescription(schedule.WeekDay),
            PairNumber = schedule.PairNumber,
            Parity = schedule.Parity,
            SubjectType = schedule.SubjectType.ToString(),
            SubjectTypeRu = AttributeRepository<SubjectType>.GetDescription(schedule.SubjectType),
            SubjectId = schedule.SubjectId,
            TeacherId = schedule.TeacherId,
            Cabinet = schedule.Cabinet
        };
}