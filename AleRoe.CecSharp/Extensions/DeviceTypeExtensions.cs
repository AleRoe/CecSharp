using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AleRoe.CecSharp.Model;

namespace AleRoe.CecSharp.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="DeviceType"/>
    /// </summary>
    public static class DeviceTypeExtensions
    {
        /// <summary>
        /// Gets <c>LogicalAddress</c> values for the given <c>DeviceType</c>
        /// </summary>
        /// <param name="value">The <see cref="DeviceType"/>.</param>
        /// <returns>An <see cref="IEnumerable{LogicalAddress}"/>.</returns>
        public static IEnumerable<LogicalAddress> GetLogicalAddresses(this DeviceType value)
        {
            var result = typeof(LogicalAddress).GetFields()
                .OrderBy(x => x.Name)
                .Where(f => f.WithAttributeValue<DeviceTypeAttribute>(x => x.DeviceType.Equals(value)))
                .Select(f => Enum.Parse<LogicalAddress>(f.Name));

            return result;
        }

        /// <summary>
        /// Gets <c>LogicalAddress</c> values for the given <c>DeviceType</c>
        /// </summary>
        /// <param name="value">The <see cref="DeviceType"/>.</param>
        /// <param name="moveToTop">The <c>LogicalAddress</c> value which should be moved to the top of the list.</param>
        /// <returns>An <see cref="IEnumerable{LogicalAddress}"/>.</returns>
        public static IEnumerable<LogicalAddress> GetLogicalAddresses(this DeviceType value, LogicalAddress moveToTop)
        {
            var result = value.GetLogicalAddresses();
            if (moveToTop != LogicalAddress.Unregistered) result = result.MoveToTop(x => x == moveToTop);
            return result;
        }

        private static bool WithAttributeValue<T>(this FieldInfo value, Func<T, bool> predicate) where T : Attribute
        {
            var attr = value.GetCustomAttribute<T>();
            if (attr == null)
                return false;
            return predicate(attr);
        }

        private static IEnumerable<T> MoveToTop<T>(this IEnumerable<T> value, Func<T, bool> func)
        {
            var list = value.ToList();
            return list.Where(func).Concat(list.Where(item => !func(item)));
        }
    }
}