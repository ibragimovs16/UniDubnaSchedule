using UniDubnaSchedule.Domain.Models;
using UniDubnaSchedule.Domain.Response;

namespace UniDubnaSchedule.Services.Abstractions;

public interface IBellsScheduleService : IBaseService<BellsSchedule>
{
    Task<BaseResponse<BellsSchedule>> GetByPairNumber(int pairNumber);
}