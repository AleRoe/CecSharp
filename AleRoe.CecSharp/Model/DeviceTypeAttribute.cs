using System;

namespace AleRoe.CecSharp.Model
{
    /// <summary>
    /// An attribute used to map a <see cref="LogicalAddress"/> with its corresponding <see cref="DeviceType"/>.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    internal class DeviceTypeAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceTypeAttribute"/> class.
        /// </summary>
        /// <param name="deviceType">The <see cref="DeviceType"/> of the device.</param>
        public DeviceTypeAttribute(DeviceType deviceType)
        {
            DeviceType = deviceType;
        }

        /// <summary>
        /// Gets the type of the device.
        /// </summary>
        /// <value>
        /// A <see cref="DeviceType"/> value.
        /// </value>
        public DeviceType DeviceType { get; }
    }
}