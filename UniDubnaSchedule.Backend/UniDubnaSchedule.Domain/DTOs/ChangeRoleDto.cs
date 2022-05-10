using UniDubnaSchedule.Domain.Enums;

namespace UniDubnaSchedule.Domain.DTOs;

public class ChangeRoleDto
{
    public string Username { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}