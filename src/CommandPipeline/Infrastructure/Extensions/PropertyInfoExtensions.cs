namespace CommandPipeline.Infrastructure.Extensions
{
    using System.Reflection;

    public static class PropertyInfoExtensions
    {
        public static void SetValue<T>(this PropertyInfo propertyInfo, object target, T value)
        {
            propertyInfo.SetValue(target, value, null);
        }
    }
}