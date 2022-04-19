using System.Linq;
using System.Net;
using Microsoft.EntityFrameworkCore;
using UniDubnaSchedule.DAL;
using UniDubnaSchedule.DAL.Repositories;
using UniDubnaSchedule.Domain.Models;
using UniDubnaSchedule.Services.Implementations;
using Xunit;

namespace UniDubnaSchedule.Tests.Services;

public class BellsScheduleServiceTests
{
    private readonly ApplicationDbContext _context;
    private readonly BellsScheduleService _service;
    
    public BellsScheduleServiceTests()
    {
        _context = new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "BellsScheduleServiceTestsDb")
                .Options);
        var repository = new BellsScheduleRepository(_context);
        _service = new BellsScheduleService(repository);
    }

    [Fact]
    public async void GetAllWithZeroRecords()
    {
        var bellsScheduleResponse = await _service.GetAllAsync();
        
        Assert.Equal(HttpStatusCode.NotFound, bellsScheduleResponse.StatusCode);
        Assert.Equal(
            "Count = 0.",
            bellsScheduleResponse.Description
        );
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

        var bellsScheduleResponse = await _service.GetAllAsync();
        
        Assert.Equal(HttpStatusCode.OK, bellsScheduleResponse.StatusCode);
        Assert.Equal(testBellsScheduleRecord.PairNumber, bellsScheduleResponse.Data!.First().PairNumber);
        Assert.Equal(testBellsScheduleRecord.Start, bellsScheduleResponse.Data!.First().Start);
        Assert.Equal(testBellsScheduleRecord.End, bellsScheduleResponse.Data!.First().End);

        await _context.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async void GetByPairNumberWithZeroRecords()
    {
        var bellsScheduleResponse = await _service.GetByPairNumber(1);
        
        Assert.Equal(HttpStatusCode.NotFound, bellsScheduleResponse.StatusCode);
        Assert.Equal(
            "The element was not found.",
            bellsScheduleResponse.Description
        );
    }

    [Fact]
    public async void GetByPairNumberWithOneRecord()
    {
        var testBellsScheduleRecord = new BellsSchedule
        {
            PairNumber = 1,
            Start = "9:00",
            End = "10:30"
        };

        await _context.AddAsync(testBellsScheduleRecord);
        await _context.SaveChangesAsync();

        var bellsScheduleResponse = await _service.GetByPairNumber(1);
        
        Assert.Equal(HttpStatusCode.OK, bellsScheduleResponse.StatusCode);
        Assert.Equal(testBellsScheduleRecord.PairNumber, bellsScheduleResponse.Data!.PairNumber);
        Assert.Equal(testBellsScheduleRecord.Start, bellsScheduleResponse.Data!.Start);
        Assert.Equal(testBellsScheduleRecord.End, bellsScheduleResponse.Data!.End);

        await _context.Database.EnsureDeletedAsync();
    }
}