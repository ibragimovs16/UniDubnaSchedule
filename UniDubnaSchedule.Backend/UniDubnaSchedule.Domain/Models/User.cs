using System.ComponentModel.DataAnnotations;
using UniDubnaSchedule.Domain.Enums;

namespace UniDubnaSchedule.Domain.Models;

public class User
{
    [Key]
    public string Username { get; set; } = string.Empty;
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
    public DateTime Registered { get; set; }
    public Roles Role { get; set; }
}