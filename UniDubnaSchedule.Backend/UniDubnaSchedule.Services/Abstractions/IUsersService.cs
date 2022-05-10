using UniDubnaSchedule.Domain.Models;
using UniDubnaSchedule.Domain.Response;

namespace UniDubnaSchedule.Services.Abstractions;

public interface IUsersService : IBaseService<User>
{
    Task<BaseResponse<string>> RemoveUser(string username);
    Task<BaseResponse<string>> ChangeRole(string username, string role);
}