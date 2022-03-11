using UniDubnaSchedule.DAL.Repositories;
using UniDubnaSchedule.Domain.DTOs;
using UniDubnaSchedule.Domain.Enums;
using UniDubnaSchedule.Domain.Models;

namespace UniDubnaSchedule.Services.Extensions;

public static class JoinedScheduleExtensions
{
    public static JoinedScheduleDto AsDto(this JoinedSchedule joinedSchedule) =>
        new()
        {
            Faculty = joinedSchedule.Faculty.ToString(),
            FacultyRu = AttributeRepository<Faculty>.GetDescription(joinedSchedule.Faculty),
            Group = joinedSchedule.Group,
            WeekDay = joinedSchedule.WeekDay.ToString(),
            WeekDayRu = AttributeRepository<WeekDay>.GetDescription(joinedSchedule.WeekDay),
            Parity = joinedSchedule.Parity,
            PairNumber = joinedSchedule.PairNumber,
            SubjectType = joinedSchedule.SubjectType.ToString(),
            SubjectTypeRu = AttributeRepository<SubjectType>.GetDescription(joinedSchedule.SubjectType),
            SubjectName = joinedSchedule.SubjectName,
            SubjectAbbreviation = joinedSchedule.SubjectAbbreviation,
            TeacherSurname = joinedSchedule.TeacherSurname,
            TeacherName = joinedSchedule.TeacherName,
            TeacherMiddleName = joinedSchedule.TeacherMiddleName,
            TeacherEmail = joinedSchedule.TeacherEmail,
            Cabinet = joinedSchedule.Cabinet,
            StartTime = joinedSchedule.StartTime,
            EndTime = joinedSchedule.EndTime
        };
}