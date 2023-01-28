using System;
using AleRoe.CecSharp.Model;

namespace AleRoe.CecSharp
{
    public interface ICecDevice
    {
        /// <summary>
        /// Gets the OSD name of the device.
        /// </summary>
        string OsdName { get; }

        /// <summary>
        /// Gets the vendor identifier.
        /// </summary>
        int VendorId { get; }

        /// <summary>
        /// Gets the type of the device.
        /// </summary>
        DeviceType DeviceType { get; }

        /// <summary>
        /// Gets or sets the logical device address.
        /// </summary>
        LogicalAddress LogicalAddress { get; set; }

        /// <summary>
        /// Gets or sets the physical device address.
        /// </summary>
        PhysicalAddress PhysicalAddress { get; set; }

        /// <summary>
        /// Gets a value indicating whether this device is the active source.
        /// </summary>
        bool IsActiveSource { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the physical address should be locked.
        /// </summary>
        bool LockPhysicalAddress { get; set; }

        /// <summary>
        /// Gets or sets the power status of the device.
        /// </summary>
        PowerStatus PowerStatus { get; set; }

        /// <summary>
        /// Processes an incoming CecMessage according to CEC Specifications
        /// </summary>
        /// <param name="message">A <see cref="CecMessage"/> request message.</param>
        /// <returns>An appropriate <see cref="CecMessage"/> response message based on the request message or <see cref="CecMessage.None"/> for noop requests.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        CecMessage ProcessCecMessage(CecMessage message);
    }
}