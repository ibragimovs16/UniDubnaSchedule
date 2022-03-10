using UniDubnaSchedule.Domain.Enums;

namespace UniDubnaSchedule.Domain.Response;

public class BaseResponse<T>
{
    public string Description { get; set; } = string.Empty;
    public StatusCode StatusCode { get; set; }
    public T? Data { get; set; }
}