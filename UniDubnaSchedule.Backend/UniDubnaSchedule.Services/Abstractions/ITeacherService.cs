using UniDubnaSchedule.Domain.Models;
using UniDubnaSchedule.Domain.Response;

namespace UniDubnaSchedule.Services.Abstractions;

public interface ITeacherService : IBaseService<Teacher>
{
    Task<BaseResponse<List<Teacher>>> GetBySurname(string surname);
}