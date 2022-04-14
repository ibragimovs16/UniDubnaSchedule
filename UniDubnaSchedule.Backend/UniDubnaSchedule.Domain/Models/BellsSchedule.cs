using System.ComponentModel.DataAnnotations;

namespace UniDubnaSchedule.Domain.Models;

/// <summary>
/// Model for the Bells table.
/// </summary>
public class BellsSchedule
{
    [Key]
    public int PairNumber { get; set; }
    public string? Start { get; set; }
    public string? End { get; set; }
}