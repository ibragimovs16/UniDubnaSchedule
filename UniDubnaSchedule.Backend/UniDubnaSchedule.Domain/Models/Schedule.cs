using System.Collections;
using UniDubnaSchedule.Domain.Enums;

namespace UniDubnaSchedule.Domain.Models;

/// <summary>
/// Basic schedule model
/// </summary>
public class Schedule
{
    public int Id { get; set; }
    public Faculty Faculty { get; set; }
    public int Group { get; set; }
    public WeekDay WeekDay { get; set; }
    public int PairNumber { get; set; }
    public int Parity { get; set; }
    public SubjectType SubjectType { get; set; }
    public int SubjectId { get; set; }
    public int TeacherId { get; set; }
    public string Cabinet { get; set; } = string.Empty;
}