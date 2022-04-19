using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UniDubnaSchedule.DAL;
using UniDubnaSchedule.DAL.Repositories;
using UniDubnaSchedule.Domain.Enums;
using UniDubnaSchedule.Domain.Models;
using Xunit;

namespace UniDubnaSchedule.Tests.Repositories;

public class ScheduleRepositoryTests
{

    [Fact]
    public async void GetAllScheduleWithZeroRecords()
    {
        var context = GetContext();

        var repository = new ScheduleRepository(context);
        var schedule = await repository.GetAllAsync();
        
        Assert.Empty(schedule);
    }
    
    [Fact]
    public async void GetAllScheduleWithOneRecord()
    {
        var context = GetContext();

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

        await context.Schedule.AddAsync(testScheduleRecord);
        await context.SaveChangesAsync();

        var repository = new ScheduleRepository(context);
        var schedule = await repository.GetAllAsync();
        
        Assert.Equal(testScheduleRecord.Id, schedule[0].Id);
        Assert.Equal(testScheduleRecord.Faculty, schedule[0].Faculty);
        Assert.Equal(testScheduleRecord.Group, schedule[0].Group);
        Assert.Equal(testScheduleRecord.WeekDay, schedule[0].WeekDay);
        Assert.Equal(testScheduleRecord.PairNumber, schedule[0].PairNumber);
        Assert.Equal(testScheduleRecord.Parity, schedule[0].Parity);
        Assert.Equal(testScheduleRecord.SubjectType, schedule[0].SubjectType);
        Assert.Equal(testScheduleRecord.SubjectId, schedule[0].SubjectId);
        Assert.Equal(testScheduleRecord.TeacherId, schedule[0].TeacherId);
        Assert.Equal(testScheduleRecord.Cabinet, schedule[0].Cabinet);

        await context.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async void GetAllScheduleWithManyRecords()
    {
        var context = GetContext();

        var testScheduleRecords = new List<Schedule>
        {
            new()
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
            },
            new()
            {
                Id = 2,
                Faculty = Faculty.ISAM,
                Group = 2221,
                WeekDay = WeekDay.Monday,
                PairNumber = 2,
                Parity = -1,
                SubjectType = SubjectType.Seminar,
                SubjectId = 2,
                TeacherId = 2,
                Cabinet = "2-111"
            }
        };
        
        await context.Schedule.AddRangeAsync(testScheduleRecords);
        await context.SaveChangesAsync();

        var repository = new ScheduleRepository(context);
        var schedule = await repository.GetAllAsync();

        for (var i = 0; i < schedule.Count; i++)
        {
            Assert.Equal(testScheduleRecords[i].Id, schedule[i].Id);
            Assert.Equal(testScheduleRecords[i].Faculty, schedule[i].Faculty);
            Assert.Equal(testScheduleRecords[i].Group, schedule[i].Group);
            Assert.Equal(testScheduleRecords[i].WeekDay, schedule[i].WeekDay);
            Assert.Equal(testScheduleRecords[i].PairNumber, schedule[i].PairNumber);
            Assert.Equal(testScheduleRecords[i].Parity, schedule[i].Parity);
            Assert.Equal(testScheduleRecords[i].SubjectType, schedule[i].SubjectType);
            Assert.Equal(testScheduleRecords[i].SubjectId, schedule[i].SubjectId);
            Assert.Equal(testScheduleRecords[i].TeacherId, schedule[i].TeacherId);
            Assert.Equal(testScheduleRecords[i].Cabinet, schedule[i].Cabinet);
        }
        
        await context.Database.EnsureDeletedAsync();
    }
    
    [Fact]
    public async void GetByIdWithNonExistId()
    {
        var context = GetContext();
        var repository = new ScheduleRepository(context);
        var schedule = await repository.GetByIdAsync(1);
        
        Assert.Null(schedule);
    }

    [Fact]
    public async void GetById()
    {
        var context = GetContext();
        
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

        await context.Schedule.AddAsync(testScheduleRecord);
        await context.SaveChangesAsync();

        var repository = new ScheduleRepository(context);
        var schedule = await repository.GetByIdAsync(1);
        
        Assert.Equal(testScheduleRecord.Id, schedule!.Id);
        Assert.Equal(testScheduleRecord.Faculty, schedule.Faculty);
        Assert.Equal(testScheduleRecord.Group, schedule.Group);
        Assert.Equal(testScheduleRecord.WeekDay, schedule.WeekDay);
        Assert.Equal(testScheduleRecord.PairNumber, schedule.PairNumber);
        Assert.Equal(testScheduleRecord.Parity, schedule.Parity);
        Assert.Equal(testScheduleRecord.SubjectType, schedule.SubjectType);
        Assert.Equal(testScheduleRecord.SubjectId, schedule.SubjectId);
        Assert.Equal(testScheduleRecord.TeacherId, schedule.TeacherId);
        Assert.Equal(testScheduleRecord.Cabinet, schedule.Cabinet);

        await context.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async void IsGroupExistsWithNonExistGroup()
    {
        var context = GetContext();
        var repository = new ScheduleRepository(context);
        var isExists = await repository.IsGroupExists(1);
        
        Assert.False(isExists);
    }

    [Fact]
    public async void IsGroupExists()
    {
        var context = GetContext();
        
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

        var repository = new ScheduleRepository(context);

        await context.AddAsync(testScheduleRecord);
        await context.SaveChangesAsync();
        
        var isExists = await repository.IsGroupExists(2221);

        Assert.True(isExists);
        
        await context.Database.EnsureDeletedAsync();
    }
    
    [Fact]
    public async void GetAllJoinedScheduleWithZeroRecords()
    {
        var context = GetContext();
        var repository = new ScheduleRepository(context);

        var joinedSchedule = await repository.GetAllJoinedScheduleAsync();
        
        Assert.Empty(joinedSchedule);
    }

    [Fact]
    public async void GetAllJoinedScheduleWithOneRecord()
    {
        var context = GetContext();
        var repository = new ScheduleRepository(context);
        
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

        await context.Schedule.AddAsync(testScheduleRecord);
        await context.Subjects.AddAsync(testSubjectRecord);
        await context.Teachers.AddAsync(testTeacherRecord);
        await context.BellsSchedule.AddAsync(testBellsScheduleRecord);

        await context.SaveChangesAsync();

        var joinedSchedule = await repository.GetAllJoinedScheduleAsync();
        
        Assert.Equal(testJoinedSchedule.Faculty, joinedSchedule.First().Faculty);
        Assert.Equal(testJoinedSchedule.Group, joinedSchedule.First().Group);
        Assert.Equal(testJoinedSchedule.WeekDay, joinedSchedule.First().WeekDay);
        Assert.Equal(testJoinedSchedule.Parity, joinedSchedule.First().Parity);
        Assert.Equal(testJoinedSchedule.PairNumber, joinedSchedule.First().PairNumber);
        Assert.Equal(testJoinedSchedule.SubjectType, joinedSchedule.First().SubjectType);
        Assert.Equal(testJoinedSchedule.SubjectName, joinedSchedule.First().SubjectName);
        Assert.Equal(testJoinedSchedule.SubjectAbbreviation, joinedSchedule.First().SubjectAbbreviation);
        Assert.Equal(testJoinedSchedule.TeacherSurname, joinedSchedule.First().TeacherSurname);
        Assert.Equal(testJoinedSchedule.TeacherName, joinedSchedule.First().TeacherName);
        Assert.Equal(testJoinedSchedule.TeacherMiddleName, joinedSchedule.First().TeacherMiddleName);
        Assert.Equal(testJoinedSchedule.TeacherEmail, joinedSchedule.First().TeacherEmail);
        Assert.Equal(testJoinedSchedule.Cabinet, joinedSchedule.First().Cabinet);
        Assert.Equal(testJoinedSchedule.StartTime, joinedSchedule.First().StartTime);
        Assert.Equal(testJoinedSchedule.EndTime, joinedSchedule.First().EndTime);
        
        await context.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async void GetAllJoinedScheduleByGroupWithZeroRecords()
    {
        var context = GetContext();
        var repository = new ScheduleRepository(context);

        var joinedSchedule = await repository.GetJoinedScheduleByGroupAsync(2221);
        
        Assert.Empty(joinedSchedule);
    }

    [Fact]
    public async void GetAllJoinedScheduleByGroupWithNonExistGroup()
    {
        var context = GetContext();
        var repository = new ScheduleRepository(context);

        var joinedSchedule = await repository.GetJoinedScheduleByGroupAsync(2222);
        Assert.Empty(joinedSchedule);
    }

    [Fact]
    public async void GetAllJoinedScheduleByGroup()
    {
        var context = GetContext();
        var repository = new ScheduleRepository(context);
        
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

        await context.Schedule.AddAsync(testScheduleRecord);
        await context.Subjects.AddAsync(testSubjectRecord);
        await context.Teachers.AddAsync(testTeacherRecord);
        await context.BellsSchedule.AddAsync(testBellsScheduleRecord);

        await context.SaveChangesAsync();

        var joinedSchedule = await repository.GetJoinedScheduleByGroupAsync(2221);
        
        Assert.Equal(testJoinedSchedule.Faculty, joinedSchedule.First().Faculty);
        Assert.Equal(testJoinedSchedule.Group, joinedSchedule.First().Group);
        Assert.Equal(testJoinedSchedule.WeekDay, joinedSchedule.First().WeekDay);
        Assert.Equal(testJoinedSchedule.Parity, joinedSchedule.First().Parity);
        Assert.Equal(testJoinedSchedule.PairNumber, joinedSchedule.First().PairNumber);
        Assert.Equal(testJoinedSchedule.SubjectType, joinedSchedule.First().SubjectType);
        Assert.Equal(testJoinedSchedule.SubjectName, joinedSchedule.First().SubjectName);
        Assert.Equal(testJoinedSchedule.SubjectAbbreviation, joinedSchedule.First().SubjectAbbreviation);
        Assert.Equal(testJoinedSchedule.TeacherSurname, joinedSchedule.First().TeacherSurname);
        Assert.Equal(testJoinedSchedule.TeacherName, joinedSchedule.First().TeacherName);
        Assert.Equal(testJoinedSchedule.TeacherMiddleName, joinedSchedule.First().TeacherMiddleName);
        Assert.Equal(testJoinedSchedule.TeacherEmail, joinedSchedule.First().TeacherEmail);
        Assert.Equal(testJoinedSchedule.Cabinet, joinedSchedule.First().Cabinet);
        Assert.Equal(testJoinedSchedule.StartTime, joinedSchedule.First().StartTime);
        Assert.Equal(testJoinedSchedule.EndTime, joinedSchedule.First().EndTime);
        
        await context.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async void GetAllJoinedScheduleByGroupAndWeekDayWithZeroRecords()
    {
        var context = GetContext();
        var repository = new ScheduleRepository(context);
        
        var joinedSchedule = await repository.GetJoinedScheduleByGroupAndWeekDayAsync(2221, 1);
        
        Assert.Empty(joinedSchedule);
    }

    [Fact]
    public async void GetAllJoinedScheduleByGroupAndWeekDay()
    {
        var context = GetContext();
        var repository = new ScheduleRepository(context);

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

        await context.Schedule.AddAsync(testScheduleRecord);
        await context.Subjects.AddAsync(testSubjectRecord);
        await context.Teachers.AddAsync(testTeacherRecord);
        await context.BellsSchedule.AddAsync(testBellsScheduleRecord);

        await context.SaveChangesAsync();

        var joinedSchedule = await repository.GetJoinedScheduleByGroupAsync(2221);

        Assert.Equal(testJoinedSchedule.Faculty, joinedSchedule.First().Faculty);
        Assert.Equal(testJoinedSchedule.Group, joinedSchedule.First().Group);
        Assert.Equal(testJoinedSchedule.WeekDay, joinedSchedule.First().WeekDay);
        Assert.Equal(testJoinedSchedule.Parity, joinedSchedule.First().Parity);
        Assert.Equal(testJoinedSchedule.PairNumber, joinedSchedule.First().PairNumber);
        Assert.Equal(testJoinedSchedule.SubjectType, joinedSchedule.First().SubjectType);
        Assert.Equal(testJoinedSchedule.SubjectName, joinedSchedule.First().SubjectName);
        Assert.Equal(testJoinedSchedule.SubjectAbbreviation, joinedSchedule.First().SubjectAbbreviation);
        Assert.Equal(testJoinedSchedule.TeacherSurname, joinedSchedule.First().TeacherSurname);
        Assert.Equal(testJoinedSchedule.TeacherName, joinedSchedule.First().TeacherName);
        Assert.Equal(testJoinedSchedule.TeacherMiddleName, joinedSchedule.First().TeacherMiddleName);
        Assert.Equal(testJoinedSchedule.TeacherEmail, joinedSchedule.First().TeacherEmail);
        Assert.Equal(testJoinedSchedule.Cabinet, joinedSchedule.First().Cabinet);
        Assert.Equal(testJoinedSchedule.StartTime, joinedSchedule.First().StartTime);
        Assert.Equal(testJoinedSchedule.EndTime, joinedSchedule.First().EndTime);

        await context.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async void Add()
    {
        var context = GetContext();
        var repository = new ScheduleRepository(context);

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

        var entityId = await repository.AddAsync(testScheduleRecord);
        
        Assert.Equal(1, entityId);

        await context.Database.EnsureDeletedAsync();
    }

    private ApplicationDbContext GetContext()
    {
        var option = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "ScheduleTestsDb")
            .Options;

        return new ApplicationDbContext(option);
    }
}