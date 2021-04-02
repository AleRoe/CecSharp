using System;
using System.Reflection;

namespace AleRoe.CecSharp.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="System.Enum"/> types.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the attribute value for an enum field.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value) ?? throw new InvalidOperationException();
            return type.GetField(name)?.GetCustomAttribute<TAttribute>();
        }
    }
}