using System.Linq;
using Microsoft.EntityFrameworkCore;
using UniDubnaSchedule.DAL;
using UniDubnaSchedule.DAL.Repositories;
using UniDubnaSchedule.Domain.Models;
using Xunit;

namespace UniDubnaSchedule.Tests.Repositories;

public class BellsScheduleRepositoryTests
{
    private readonly ApplicationDbContext _context;
    private readonly BellsScheduleRepository _repository;

    public BellsScheduleRepositoryTests()
    {
        _context = new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "BellsScheduleTestsDb")
                .Options);
        _repository = new BellsScheduleRepository(_context);
    }

    [Fact]
    public async void GetAllWithZeroRecords()
    {
        var bellsSchedule = await _repository.GetAllAsync();
        
        Assert.Empty(bellsSchedule);
    }

    [Fact]
    public async void GetAllWithOneRecord()
    {
        var testBellsScheduleRecord = new BellsSchedule
        {
            PairNumber = 1,
            Start = "9:00",
            End = "10:30"
        };

        await _context.AddAsync(testBellsScheduleRecord);
        await _context.SaveChangesAsync();

        var bellsSchedule = await _repository.GetAllAsync();
        
        Assert.Equal(testBellsScheduleRecord.PairNumber, bellsSchedule.First().PairNumber);
        Assert.Equal(testBellsScheduleRecord.Start, bellsSchedule.First().Start);
        Assert.Equal(testBellsScheduleRecord.End, bellsSchedule.First().End);

        await _context.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async void GetByIdWithZeroRecords()
    {
        var bellsSchedule = await _repository.GetByIdAsync(1);
        
        Assert.Null(bellsSchedule);
    }

    [Fact]
    public async void GetByIdWithOneRecord()
    {
        var testBellsScheduleRecord = new BellsSchedule
        {
            PairNumber = 1,
            Start = "9:00",
            End = "10:30"
        };

        await _context.AddAsync(testBellsScheduleRecord);
        await _context.SaveChangesAsync();

        var bellsSchedule = await _repository.GetByIdAsync(1);
        
        Assert.Equal(testBellsScheduleRecord.PairNumber, bellsSchedule!.PairNumber);
        Assert.Equal(testBellsScheduleRecord.Start, bellsSchedule.Start);
        Assert.Equal(testBellsScheduleRecord.End, bellsSchedule.End);

        await _context.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async void Add()
    {
        var testBellsScheduleRecord = new BellsSchedule
        {
            PairNumber = 1,
            Start = "9:00",
            End = "10:30"
        };

        var entityId = await _repository.AddAsync(testBellsScheduleRecord);
        
        Assert.Equal(testBellsScheduleRecord.PairNumber, entityId);

        await _context.Database.EnsureDeletedAsync();
    }
}