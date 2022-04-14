using System.ComponentModel;

namespace UniDubnaSchedule.DAL.Repositories;

public static class AttributeRepository<T>
{
    /// <summary>
    /// Getting the attribute value.
    /// </summary>
    /// <param name="data">The object whose attribute description you need to get</param>
    /// <returns>Attribute description</returns>
    public static string GetDescription(T data)
    {
        var fieldInfo = data?.GetType().GetField(data.ToString() ?? string.Empty);
        var attributes = (DescriptionAttribute[])fieldInfo!.GetCustomAttributes(
            typeof(DescriptionAttribute), false
        );

        return attributes.First().Description;
    }
}