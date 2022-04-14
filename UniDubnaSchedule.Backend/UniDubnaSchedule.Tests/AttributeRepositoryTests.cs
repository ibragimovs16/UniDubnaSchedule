using UniDubnaSchedule.DAL.Repositories;
using UniDubnaSchedule.Domain.Enums;
using Xunit;

namespace UniDubnaSchedule.Tests;

public class AttributeRepositoryTests
{
    [Fact]
    public void GetDescription()
    {
        var description = AttributeRepository<Faculty>.GetDescription(Faculty.ISAM);
        
        Assert.Equal("ИСАУ", description);
    }
}