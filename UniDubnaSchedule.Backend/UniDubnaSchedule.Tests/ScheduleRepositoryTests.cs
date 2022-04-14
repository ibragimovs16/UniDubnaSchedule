using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UniDubnaSchedule.DAL;
using UniDubnaSchedule.DAL.Repositories;
using UniDubnaSchedule.Domain.Enums;
using UniDubnaSchedule.Domain.Models;
using Xunit;

namespace UniDubnaSchedule.Tests;

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

    // ToDo: Check the rest of the methods after implementing the add methods in the repositories.

    private ApplicationDbContext GetContext()
    {
        var option = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "ScheduleTestsDb")
            .Options;

        return new ApplicationDbContext(option);
    }
}