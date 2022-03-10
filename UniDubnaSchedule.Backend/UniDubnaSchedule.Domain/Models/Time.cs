namespace UniDubnaSchedule.Domain.Models;

// Simple class for storage time
public class Time
{
    // Get time in format "HH:mm"
    public Time(string timeRaw)
    {
        var separatedTime = timeRaw.Split(":");
        Hours = int.Parse(separatedTime[0]);
        Minutes = int.Parse(separatedTime[1]);
    }
    
    public int Hours { get; }
    public int Minutes { get; }
    
    // Return time in format "HH:mm"
    public override string ToString() =>
        Hours < 10 ? "0" : "" + $"{Hours}:{Minutes}";
}