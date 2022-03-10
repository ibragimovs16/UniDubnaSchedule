namespace UniDubnaSchedule.Domain.Models;

public class BellsSchedule
{
    public int PairNumber { get; set; }
    public Time? Start { get; set; }
    public Time? End { get; set; }
}