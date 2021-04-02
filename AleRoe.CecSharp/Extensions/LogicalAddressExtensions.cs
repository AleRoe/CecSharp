using AleRoe.CecSharp.Model;

namespace AleRoe.CecSharp.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="LogicalAddress"/>.
    /// </summary>
    public static class LogicalAddressExtensions
    {
        /// <summary>
        /// Returns the <c>DeviceType</c> for a given <c>LogicalAddress</c>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <c>DeviceType</c> of the <c>LogicalAddress</c>.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public static DeviceType GetDeviceType(this LogicalAddress value)
        {
            var attr = value.GetAttribute<DeviceTypeAttribute>();
            if (attr == null) return default;
            return attr.DeviceType;
        }

        /// <summary>
        /// Returns a string representation of the given <c>LogicalAddress</c>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string AsString(this LogicalAddress value)
        {
            return value.ToString("X").TrimStart(new[] {'0'});
        }
    }
}