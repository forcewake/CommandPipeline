namespace CommandPipeline.Infrastructure.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class TypeExtensions
    {
        public static object CreateInstanceForGeneric(this Type type, Type[] genericArguments)
        {
            // Specify the type used by the generic type.
            var specific = type.MakeGenericType(genericArguments);

            // Create the final type (InArgument<>)
            var instance = Activator.CreateInstance(specific, true);

            return instance;
        }

        public static Type FindBaseGenericType(this Type type, Type baseGenericType)
        {
            for (Type t = type; t != null; t = t.BaseType)
                if (t.IsGenericType && t.GetGenericTypeDefinition() == baseGenericType)
                    return t;
            return null;
        }

        public static IEnumerable<PropertyInfo> GetAllProperties<TAttribute>(this Type type)
            where TAttribute : Attribute
        {
            if (type == null)
                return Enumerable.Empty<PropertyInfo>();

            const BindingFlags flags = BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance |
                                       BindingFlags.DeclaredOnly;
            return type.GetProperties(flags).Where(p => p.GetAttribute<TAttribute>() != null).Union(GetAllProperties<TAttribute>(type.BaseType));
        }

        /// <summary>
        /// Get an attribute for a property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyinfo"></param>
        /// <returns></returns>
        public static T GetAttribute<T>(this PropertyInfo propertyinfo)
            where T : Attribute
        {
            var attributes = propertyinfo.GetCustomAttributes(typeof(T), false).FirstOrDefault();
            return (T)attributes;
        }
    }
}
