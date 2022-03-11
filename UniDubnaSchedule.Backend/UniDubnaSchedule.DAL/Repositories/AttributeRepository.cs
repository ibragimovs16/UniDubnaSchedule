using System.ComponentModel;

namespace UniDubnaSchedule.DAL.Repositories;

public static class AttributeRepository<T>
{
    public static string GetDescription(T data)
    {
        var fieldInfo = data?.GetType().GetField(data.ToString() ?? string.Empty);
        var attributes = (DescriptionAttribute[])fieldInfo!.GetCustomAttributes(
            typeof(DescriptionAttribute), false
        );

        return attributes.First().Description;
    }
}