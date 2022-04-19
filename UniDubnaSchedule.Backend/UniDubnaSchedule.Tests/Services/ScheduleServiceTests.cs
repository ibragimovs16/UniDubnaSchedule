using System.Linq;
using System.Net;
using Microsoft.EntityFrameworkCore;
using UniDubnaSchedule.DAL;
using UniDubnaSchedule.DAL.Repositories;
using UniDubnaSchedule.Domain.Enums;
using UniDubnaSchedule.Domain.Models;
using UniDubnaSchedule.Services.Implementations;
using Xunit;

namespace UniDubnaSchedule.Tests.Services;

public class ScheduleServiceTests
{
    private readonly ApplicationDbContext _context;
    private readonly ScheduleService _service;

    public ScheduleServiceTests()
    {
        _context = new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ScheduleServiceTestsDb")
                .Options);
        var repository = new ScheduleRepository(_context);
        _service = new ScheduleService(repository);
    }
    
    [Fact]
    public async void GetAllWithZeroRecords()
    {
        var scheduleResponse = await _service.GetAllAsync();
        
        Assert.Equal(HttpStatusCode.NotFound, scheduleResponse.StatusCode);
        Assert.Equal("Count = 0.", scheduleResponse.Description);
    }

    [Fact]
    public async void GetAllWithOneRecord()
    {
        var testScheduleRecord = new Schedule
        {
            Id = 1,
            Faculty = Faculty.ISAM,
            Group = 2221,
            WeekDay = WeekDay.Monday,
            PairNumber = 1,
            Parity = -1,
            SubjectType = SubjectType.Seminar,
            SubjectId = 1,
            TeacherId = 1,
            Cabinet = "1-111"
        };

        var testSubjectRecord = new Subject
        {
            Id = 1,
            Name = "Subject",
            Abbreviation = null
        };

        var testTeacherRecord = new Teacher
        {
            Id = 1,
            Surname = "Surname",
            Name = "Name",
            MiddleName = null,
            Email = null
        };

        var testBellsScheduleRecord = new BellsSchedule
        {
            PairNumber = 1,
            Start = "9:00",
            End = "10:30"
        };

        var testJoinedSchedule = new JoinedSchedule
        {
            Faculty = Faculty.ISAM,
            Group = 2221,
            WeekDay = WeekDay.Monday,
            Parity = -1,
            PairNumber = 1,
            SubjectType = SubjectType.Seminar,
            SubjectName = "Subject",
            SubjectAbbreviation = null,
            TeacherSurname = "Surname",
            TeacherName = "Name",
            TeacherMiddleName = null,
            TeacherEmail = null,
            Cabinet = "1-111",
            StartTime = "9:00",
            EndTime = "10:30"
        };

        await _context.Schedule.AddAsync(testScheduleRecord);
        await _context.Subjects.AddAsync(testSubjectRecord);
        await _context.Teachers.AddAsync(testTeacherRecord);
        await _context.BellsSchedule.AddAsync(testBellsScheduleRecord);

        await _context.SaveChangesAsync();
        
        var scheduleResponse = await _service.GetAllAsync();

        Assert.Equal(HttpStatusCode.OK, scheduleResponse.StatusCode);
        Assert.Equal(testJoinedSchedule.Faculty, scheduleResponse.Data!.First().Faculty);
        Assert.Equal(testJoinedSchedule.Group, scheduleResponse.Data!.First().Group);
        Assert.Equal(testJoinedSchedule.WeekDay, scheduleResponse.Data!.First().WeekDay);
        Assert.Equal(testJoinedSchedule.Parity, scheduleResponse.Data!.First().Parity);
        Assert.Equal(testJoinedSchedule.PairNumber, scheduleResponse.Data!.First().PairNumber);
        Assert.Equal(testJoinedSchedule.SubjectType, scheduleResponse.Data!.First().SubjectType);
        Assert.Equal(testJoinedSchedule.SubjectName, scheduleResponse.Data!.First().SubjectName);
        Assert.Equal(testJoinedSchedule.SubjectAbbreviation, scheduleResponse.Data!.First().SubjectAbbreviation);
        Assert.Equal(testJoinedSchedule.TeacherSurname, scheduleResponse.Data!.First().TeacherSurname);
        Assert.Equal(testJoinedSchedule.TeacherName, scheduleResponse.Data!.First().TeacherName);
        Assert.Equal(testJoinedSchedule.TeacherMiddleName, scheduleResponse.Data!.First().TeacherMiddleName);
        Assert.Equal(testJoinedSchedule.TeacherEmail, scheduleResponse.Data!.First().TeacherEmail);
        Assert.Equal(testJoinedSchedule.Cabinet, scheduleResponse.Data!.First().Cabinet);
        Assert.Equal(testJoinedSchedule.StartTime, scheduleResponse.Data!.First().StartTime);
        Assert.Equal(testJoinedSchedule.EndTime, scheduleResponse.Data!.First().EndTime);

        await _context.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async void GetAllByGroupWithZeroRecords()
    {
        var scheduleResponse = await _service.GetJoinedScheduleByGroupAsync(2221);
        
        Assert.Equal(HttpStatusCode.NotFound, scheduleResponse.StatusCode);
        Assert.Equal(
            "The group number is entered incorrectly or is missing from the database.",
            scheduleResponse.Description
        );
    }

    [Fact]
    public async void GetAllByGroup()
    {
        var testScheduleRecord = new Schedule
        {
            Id = 1,
            Faculty = Faculty.ISAM,
            Group = 2221,
            WeekDay = WeekDay.Monday,
            PairNumber = 1,
            Parity = -1,
            SubjectType = SubjectType.Seminar,
            SubjectId = 1,
            TeacherId = 1,
            Cabinet = "1-111"
        };

        var testSubjectRecord = new Subject
        {
            Id = 1,
            Name = "Subject",
            Abbreviation = null
        };

        var testTeacherRecord = new Teacher
        {
            Id = 1,
            Surname = "Surname",
            Name = "Name",
            MiddleName = null,
            Email = null
        };

        var testBellsScheduleRecord = new BellsSchedule
        {
            PairNumber = 1,
            Start = "9:00",
            End = "10:30"
        };

        var testJoinedSchedule = new JoinedSchedule
        {
            Faculty = Faculty.ISAM,
            Group = 2221,
            WeekDay = WeekDay.Monday,
            Parity = -1,
            PairNumber = 1,
            SubjectType = SubjectType.Seminar,
            SubjectName = "Subject",
            SubjectAbbreviation = null,
            TeacherSurname = "Surname",
            TeacherName = "Name",
            TeacherMiddleName = null,
            TeacherEmail = null,
            Cabinet = "1-111",
            StartTime = "9:00",
            EndTime = "10:30"
        };

        await _context.Schedule.AddAsync(testScheduleRecord);
        await _context.Subjects.AddAsync(testSubjectRecord);
        await _context.Teachers.AddAsync(testTeacherRecord);
        await _context.BellsSchedule.AddAsync(testBellsScheduleRecord);

        await _context.SaveChangesAsync();
        
        var scheduleResponse = await _service.GetJoinedScheduleByGroupAsync(2221);

        Assert.Equal(HttpStatusCode.OK, scheduleResponse.StatusCode);
        Assert.Equal(testJoinedSchedule.Faculty, scheduleResponse.Data!.First().Faculty);
        Assert.Equal(testJoinedSchedule.Group, scheduleResponse.Data!.First().Group);
        Assert.Equal(testJoinedSchedule.WeekDay, scheduleResponse.Data!.First().WeekDay);
        Assert.Equal(testJoinedSchedule.Parity, scheduleResponse.Data!.First().Parity);
        Assert.Equal(testJoinedSchedule.PairNumber, scheduleResponse.Data!.First().PairNumber);
        Assert.Equal(testJoinedSchedule.SubjectType, scheduleResponse.Data!.First().SubjectType);
        Assert.Equal(testJoinedSchedule.SubjectName, scheduleResponse.Data!.First().SubjectName);
        Assert.Equal(testJoinedSchedule.SubjectAbbreviation, scheduleResponse.Data!.First().SubjectAbbreviation);
        Assert.Equal(testJoinedSchedule.TeacherSurname, scheduleResponse.Data!.First().TeacherSurname);
        Assert.Equal(testJoinedSchedule.TeacherName, scheduleResponse.Data!.First().TeacherName);
        Assert.Equal(testJoinedSchedule.TeacherMiddleName, scheduleResponse.Data!.First().TeacherMiddleName);
        Assert.Equal(testJoinedSchedule.TeacherEmail, scheduleResponse.Data!.First().TeacherEmail);
        Assert.Equal(testJoinedSchedule.Cabinet, scheduleResponse.Data!.First().Cabinet);
        Assert.Equal(testJoinedSchedule.StartTime, scheduleResponse.Data!.First().StartTime);
        Assert.Equal(testJoinedSchedule.EndTime, scheduleResponse.Data!.First().EndTime);

        await _context.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async void GetAllByGroupAndWeekDayWithZeroRecords()
    {
        var scheduleResponse = await _service.GetJoinedScheduleByGroupAndWeekDay(2221, 1);
        
        Assert.Equal(HttpStatusCode.NotFound, scheduleResponse.StatusCode);
        Assert.Equal(
            "The group number or day of the week is entered incorrectly or is missing from the database.",
            scheduleResponse.Description
        );
    }

    [Fact]
    public async void GetAllByGroupAndWeekDay()
    {
        var testScheduleRecord = new Schedule
        {
            Id = 1,
            Faculty = Faculty.ISAM,
            Group = 2221,
            WeekDay = WeekDay.Monday,
            PairNumber = 1,
            Parity = -1,
            SubjectType = SubjectType.Seminar,
            SubjectId = 1,
            TeacherId = 1,
            Cabinet = "1-111"
        };

        var testSubjectRecord = new Subject
        {
            Id = 1,
            Name = "Subject",
            Abbreviation = null
        };

        var testTeacherRecord = new Teacher
        {
            Id = 1,
            Surname = "Surname",
            Name = "Name",
            MiddleName = null,
            Email = null
        };

        var testBellsScheduleRecord = new BellsSchedule
        {
            PairNumber = 1,
            Start = "9:00",
            End = "10:30"
        };

        var testJoinedSchedule = new JoinedSchedule
        {
            Faculty = Faculty.ISAM,
            Group = 2221,
            WeekDay = WeekDay.Monday,
            Parity = -1,
            PairNumber = 1,
            SubjectType = SubjectType.Seminar,
            SubjectName = "Subject",
            SubjectAbbreviation = null,
            TeacherSurname = "Surname",
            TeacherName = "Name",
            TeacherMiddleName = null,
            TeacherEmail = null,
            Cabinet = "1-111",
            StartTime = "9:00",
            EndTime = "10:30"
        };

        await _context.Schedule.AddAsync(testScheduleRecord);
        await _context.Subjects.AddAsync(testSubjectRecord);
        await _context.Teachers.AddAsync(testTeacherRecord);
        await _context.BellsSchedule.AddAsync(testBellsScheduleRecord);

        await _context.SaveChangesAsync();
        
        var scheduleResponse = await _service.GetJoinedScheduleByGroupAndWeekDay(2221, 1);

        Assert.Equal(HttpStatusCode.OK, scheduleResponse.StatusCode);
        Assert.Equal(testJoinedSchedule.Faculty, scheduleResponse.Data!.First().Faculty);
        Assert.Equal(testJoinedSchedule.Group, scheduleResponse.Data!.First().Group);
        Assert.Equal(testJoinedSchedule.WeekDay, scheduleResponse.Data!.First().WeekDay);
        Assert.Equal(testJoinedSchedule.Parity, scheduleResponse.Data!.First().Parity);
        Assert.Equal(testJoinedSchedule.PairNumber, scheduleResponse.Data!.First().PairNumber);
        Assert.Equal(testJoinedSchedule.SubjectType, scheduleResponse.Data!.First().SubjectType);
        Assert.Equal(testJoinedSchedule.SubjectName, scheduleResponse.Data!.First().SubjectName);
        Assert.Equal(testJoinedSchedule.SubjectAbbreviation, scheduleResponse.Data!.First().SubjectAbbreviation);
        Assert.Equal(testJoinedSchedule.TeacherSurname, scheduleResponse.Data!.First().TeacherSurname);
        Assert.Equal(testJoinedSchedule.TeacherName, scheduleResponse.Data!.First().TeacherName);
        Assert.Equal(testJoinedSchedule.TeacherMiddleName, scheduleResponse.Data!.First().TeacherMiddleName);
        Assert.Equal(testJoinedSchedule.TeacherEmail, scheduleResponse.Data!.First().TeacherEmail);
        Assert.Equal(testJoinedSchedule.Cabinet, scheduleResponse.Data!.First().Cabinet);
        Assert.Equal(testJoinedSchedule.StartTime, scheduleResponse.Data!.First().StartTime);
        Assert.Equal(testJoinedSchedule.EndTime, scheduleResponse.Data!.First().EndTime);

        await _context.Database.EnsureDeletedAsync();
    }
}