using UniDubnaSchedule.Domain.Models;

namespace UniDubnaSchedule.DAL.Interfaces;

public interface IBellsScheduleRepository : IBaseRepository<BellsSchedule>
{
    Task<BellsSchedule?> GetByIdAsync(int id);
}