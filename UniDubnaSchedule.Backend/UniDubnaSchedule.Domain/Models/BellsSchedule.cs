using System.ComponentModel.DataAnnotations;

namespace UniDubnaSchedule.Domain.Models;

public class BellsSchedule
{
    [Key]
    public int PairNumber { get; set; }
    public string? Start { get; set; }
    public string? End { get; set; }
}